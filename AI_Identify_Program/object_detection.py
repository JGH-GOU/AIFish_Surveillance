##---------------   Video Object Detection Using Tensorflow-trained Classifier   -------------------------------
import os
import cv2
import numpy as np
import tensorflow as tf
import sys

os.environ["CUDA_VISIBLE_DEVICES"] = "0"

config = tf.ConfigProto()
config.gpu_options.allow_growth = True
session = tf.Session(config=config)

import time

import tkinter as tk
from tkinter import filedialog, messagebox

# This is needed since the notebook is stored in the object_detection folder.
sys.path.append("..")

# Import utilites
from utils import label_map_util
from utils import visualization_utils as vis_util

root = tk.Tk()
root.withdraw()
root.attributes("-topmost", True)
file_opt = options = {}
options['title'] = 'Please Select A Fish Image'
options['parent'] = root

# Name of the directory containing the object detection module we're using
MODEL_NAME = 'inference_graph'
VIDEO_NAME = tk.filedialog.askopenfilename(**file_opt)

# Extract video name only
VIDEO_NAME_temp = VIDEO_NAME.split('.')[0]
VIDEO_NAME_temp2 = VIDEO_NAME_temp.split('/')
VIDEO_NAME_len = int(len(VIDEO_NAME_temp2))
VIDEO_NAME_temp2 = VIDEO_NAME_temp2[VIDEO_NAME_len-1]


# Get current path to current working directory
CWD_PATH = os.getcwd()

# Path to frozen detection graph .pb file, which contains the model for object detection.
PATH_TO_CKPT = os.path.join(CWD_PATH,MODEL_NAME,'frozen_inference_graph.pb')

# Path to label map file
PATH_TO_LABELS = os.path.join(CWD_PATH,'training','labelmap.pbtxt')

# Path to video
PATH_TO_VIDEO = os.path.join(CWD_PATH,VIDEO_NAME)

# The unit of tolerate_time is second(sec)
#tolerate_time = float(sys.argv[1])
print("")
tolerate_time = float(input('Please choose N second to tolerance: '))

# Number of classes the object detector can identify
NUM_CLASSES = 2

# Load the label map.
# Label maps map indices to category names, so that when our convolution
# network predicts `5`, we know that this corresponds to `king`.
# Here we use internal utility functions, but anything that returns a
# dictionary mapping integers to appropriate string labels would be fine
label_map = label_map_util.load_labelmap(PATH_TO_LABELS)
categories = label_map_util.convert_label_map_to_categories(label_map, max_num_classes=NUM_CLASSES, use_display_name=True)
category_index = label_map_util.create_category_index(categories)

# Load the Tensorflow model into memory.
detection_graph = tf.Graph()
with detection_graph.as_default():
    od_graph_def = tf.GraphDef()
    with tf.gfile.GFile(PATH_TO_CKPT, 'rb') as fid:
        serialized_graph = fid.read()
        od_graph_def.ParseFromString(serialized_graph)
        tf.import_graph_def(od_graph_def, name='')

    sess = tf.Session(graph=detection_graph)

# Define input and output tensors (i.e. data) for the object detection classifier

# Input tensor is the image
image_tensor = detection_graph.get_tensor_by_name('image_tensor:0')

# Output tensors are the detection boxes, scores, and classes
# Each box represents a part of the image where a particular object was detected
detection_boxes = detection_graph.get_tensor_by_name('detection_boxes:0')

# Each score represents level of confidence for each of the objects.
# The score is shown on the result image, together with the class label.
detection_scores = detection_graph.get_tensor_by_name('detection_scores:0')
detection_classes = detection_graph.get_tensor_by_name('detection_classes:0')

# Number of objects detected
num_detections = detection_graph.get_tensor_by_name('num_detections:0')

def file_name_to_time(file_name):
    str_1 = "/"
    str_2 = ":"
    file_name_temp = file_name.split('_')
    year_s = file_name_temp[0]
    date_s = file_name_temp[1]
    time_s = file_name_temp[2]     
    date_list = list(date_s)
    time_list = list(time_s)
    seq_1 = (year_s, str(date_list[0]+date_list[1]), str(date_list[2]+date_list[3]))
    seq_2 = (str(time_list[0]+time_list[1]),str(time_list[2]+time_list[3]),str(time_list[4]+time_list[5]))
    result = (str_1.join(seq_1)+" "+str_2.join(seq_2))
    return result 

#--------------------------------------------------------------------------------
# Select Video Source (from File or Webcam)
#--------------------------------------------------------------------------------    
# Open video from an input file
video = cv2.VideoCapture(PATH_TO_VIDEO)
width = int(video.get(cv2.CAP_PROP_FRAME_WIDTH))
height = int(video.get(cv2.CAP_PROP_FRAME_HEIGHT))
video_FPS = int(video.get(cv2.CAP_PROP_FPS))
video_Total_Frame_Count = int(video.get(cv2.CAP_PROP_FRAME_COUNT))   

print("before resizing, width = ", width)
print("before resizing, height = ", height)

resize_flag = False
if width!=640 or height!=480:
    width = 640
    height = 480
    resize_flag = True
   
print ("after resizing, width = ", width)
print ("after resizing, height = ", height)
print ("VIDEO_FPS = ",video_FPS)
print ("VIDEO_TOTAL_FRAME = ",video_Total_Frame_Count)
print ("VIDEO_NAME = ",VIDEO_NAME_temp2)

area = width * height

N = np.zeros((0,4),  dtype=np.int)    #初始被追蹤物件的編號，被Object Detection的候選者

frame_backup_prev = np.zeros((width,height,3),  dtype=np.int)

def diffimage(src_1, src_2):
    src_1 = src_1.astype(np.int32)
    src_2 = src_2.astype(np.int32)
    diff = abs(src_1 - src_2)
    return diff

def to_gray(input_image):
    return cv2.cvtColor(image,cv2.COLOR_BGR2GRAY)

fourcc = cv2.VideoWriter_fourcc(*'XVID')
out_video_str = (VIDEO_NAME_temp2 + "_output_demo.mp4")
out = cv2.VideoWriter(str(out_video_str), fourcc, video_FPS, (640,480))

frame_count = 0
pre_frame_count = 0
max_frame_count = -1
all_frames = []

image_skip_option = -1
num_merged_frame = 2
num_ignore_frame = 0
num_capture_roi = 0
#cal_frame = 0
nn_runtime = 0
runtime_per_frame = 0
video_MSEC = 0

video_time_array = []
temp_time_array = []
temp_video_time = 0
tol_flag = True

font=cv2.FONT_HERSHEY_SIMPLEX

while(video.isOpened()):
    start_t = time.time()    
    #-------------------------------------------------------------------------------------    
    #  Acquire frame and expand frame dimensions to have shape: [1, None, None, 3]
    #  i.e. a single-column array, where each item in the column has the pixel RGB value
    #-------------------------------------------------------------------------------------     
    frame_count = frame_count + 1    
    ret, frame1 = video.read()
    video_MSEC = int(video.get(cv2.CAP_PROP_POS_MSEC))  
    if not ret:
        if (frame_count >= video_Total_Frame_Count):
            print ("FINISH !!")
        else:
            print ("READ IMAGES FAILED !!")        
        break
       
    if resize_flag:
        frame1 = cv2.resize(frame1, (width, height), interpolation=cv2.INTER_CUBIC)
    
    if max_frame_count > 0 and frame_count == max_frame_count:
        break    
        
    print ("-------------------------------Frame ",frame_count,"-------------------------------")
    print ("VIDEO_TIME =",int(video_MSEC/60000),"min",(int(video_MSEC/1000))%60,"s",video_MSEC%1000,"ms") 
    
    if frame_count == 1:
        cv2.namedWindow('Fish Identification')
        cv2.moveWindow('Fish Identification', 100,100)
    
    frame_ori = frame1.copy()
    depth_txt = "frame "+str(frame_count)
        
    ignore_frame_flag = False
    if image_skip_option == 1:
        # Option 1: Interleave 1, (1+num_merged_frame), (1+num_merged_frame*2) to deal
        if (frame_count % num_merged_frame) != 1:
            ignore_frame_flag = True
        
    if ignore_frame_flag:
        num_ignore_frame = num_ignore_frame + 1
        continue
    
    frame_expanded = np.expand_dims(frame1, axis=0)
     
    #-----------------------------------------------------------------------------
    #  Perform object detection via ML model with input image
    #-----------------------------------------------------------------------------
    (boxes, scores, classes, num) = sess.run(
        [detection_boxes, detection_scores, detection_classes, num_detections],
        feed_dict={image_tensor: frame_expanded})
    
      
    N = np.zeros((0,4),  dtype=np.int)  #初始被追蹤物件的編號
    NFish = np.zeros((0,4),  dtype=np.int)
    NAll = np.zeros((0,5),  dtype=np.int)
    NAll_list = np.zeros((0,5),  dtype=np.int)
    image,N,NFish,NAll,NAll_list=vis_util.visualize_boxes_and_labels_on_image_array(
        frame_ori,
        N,
        NFish,
        NAll,
        NAll_list,
        np.squeeze(boxes),
        np.squeeze(classes).astype(np.int32),
        np.squeeze(scores),
        category_index,
        use_normalized_coordinates=True,
        line_thickness=3,
        min_score_thresh=0.95
        )
    
    # Detect fish or not
    if (int(len(NAll_list)) > 0):        
        temp_video_time = video_MSEC
        if tol_flag==True:
            # The start_time when detect the fish.
            temp_time_array.append(video_MSEC)
            tol_flag = False
            
    # Unit: tolerate_time = second(sec) video_MSEC = ms
    if (((video_MSEC - temp_video_time)/1000) > tolerate_time and tol_flag==False):
        # The end_time when out of the tolerate_time after detecting the fish.
        temp_time_array.append(video_MSEC - (tolerate_time*1000))
        # Store start_time and end_time pair
        video_time_array.append(temp_time_array)
        temp_time_array = []
        # Start recording time interval of next fish detection.
        tol_flag = True
    
    cv2.imshow('Fish Identification',frame_ori)
    out.write(frame_ori)

    if cv2.waitKey(30) & 0xFF==27:
       out.release()
       break    

print("\n------------------------------------------")
print("num_capture_roi = ",num_capture_roi)
print("num_ignore_frame = ", num_ignore_frame)

diff_time = 0
time_interval_count = 1 

out_file_str = (VIDEO_NAME_temp2 + ".txt")

#with open("Fish_Appear_Time_Interval.txt","w") as f:
with open(out_file_str,"w") as f:
    print ("\n[Record]")
    f.write("[Record]\n")
    video_time = file_name_to_time(VIDEO_NAME_temp2)
    print ("Time=",video_time)
    str_temp = ("Time="+video_time+"\n")
    f.write(str_temp)
    print ("Length=%d\n"%(int(video_Total_Frame_Count/video_FPS)))
    f.write("Length=%d\n\n"%(int(video_Total_Frame_Count/video_FPS)))
    
    print ("[Data]") 
    f.write ("[Data]\n")   
    for i in video_time_array:        
        diff_time += (i[1]-i[0])    
    if (int(diff_time)==0):
        print("Percentage=100")
        f.write("Percentage=100\n")
    else:    
        print("Percentage=%.f"%((diff_time/video_MSEC)*100))
        f.write("Percentage=%.f\n"%((diff_time/video_MSEC)*100))
        #print("Percentage=%.4f"%((diff_time/video_MSEC)*100))
        #f.write("Percentage=%.4f\n"%((diff_time/video_MSEC)*100))
        
    #print ("Count=",len(video_time_array))
    str_temp ="Count="+str(len(video_time_array))+"\n"
    print(str_temp)
    f.write (str_temp)
    for i in video_time_array:
        print ("Bgn_",time_interval_count,"=",(int(i[0]/1000)))
        str_temp_bgn = "Bgn_"+str(time_interval_count)+"="+str(int(i[0]/1000))+"\n"
        f.write(str_temp_bgn)
        print ("End_",time_interval_count,"=",(int(i[1]/1000)))
        str_temp_End = "End_"+str(time_interval_count)+"="+str(int(i[1]/1000))+"\n"        
        f.write(str_temp_End) 
        time_interval_count += 1
    f.close()

# Clean up
out.release()
video.release()
cv2.destroyAllWindows()
#root.withdraw()
