# 雲端智能養殖魚群縮時搜尋與影像強化系統

**(1)	內建人工智能的雲端搜尋引擎系統**：內建人工智能縮時搜尋功能與自動化辨識指標，引導漁民迅速找到魚群活動較頻繁的錄像，且以AI過濾出魚隻較完整影像，可快速檢查魚群的活動狀態，減少不必要的搜尋時間浪費，讓漁民在數秒內便能找到魚群以觀察其生長動態。  
**(2)	全自動水下影像除霧技術**：全自動強化所接收到的水下影像，並結合進雲端搜索引擎中，除提高雲端搜尋引擎系統中人工智慧技術對魚群影像的辨識精準度外，也提供漁民朋友更清晰的魚隻活動影像。

# 背景與問題  
本提案發展了一套應用於養殖魚場具有雲端智能縮時搜尋功能的監控系統，
透過建置的攝影系統定期自動錄影我們可取得漁民養殖的水下影像對養殖魚進行觀察， 
然而實測使用時，漁民反映影像過多真正有魚群活動的部分少，當有狀況發生想調閱歷史錄像瞭解過去狀況時，
需要以人眼花費許多時間才能找到魚群活動的各個時間點做比較，此外不同於單純的陸地環境，在多變且雜質眾多的水下養殖環境中，
光折射與霧化等問題使得所取得的水下影像解析度經常模糊不清，造成人眼難以判斷魚群的狀況，
為了解決些問題，本提案將開發一網頁介面，讓養殖戶可以透過網頁登入搜尋魚塭水下影像的歷史紀錄，
接著回放歷史影片，對於所選取的影片，我們將在雲端運用AI智能技術自動找出影片中養殖魚與魚群活動的時間點，
透過本案所開發的智能縮時搜尋功能與辨識指標，引導漁民迅速找到並檢查魚群的活動狀態，以減少不必要的人眼搜尋時間浪費。
同時發展了一項全自動水下影像除霧技術來進一步強化所接收到的水下影像，並結合進雲端人工智能搜尋引擎提高魚群影像的辨識精準度，
除了提高雲端智能搜尋引擎搜尋的精準度外，同時也提供漁民朋友一更清晰的魚隻強化影像，以利後續的魚病分析作業。

<div align=center><img width="486" height="254" src="https://github.com/JGH-GOU/AIFish_Surveillance/blob/main/Demo_PIC/Problems.jpg"/></div>

# 開發成果  
下圖是本團隊在此提案中所開發的網頁介面，  
此介面可根據每個魚塭不同的錄影日期時間，有魚蹤影像比例等條件設置來搜尋想要觀看的影片。  
搜尋結果也可進一步排序: 點選下方表格的欄位，例如：錄影時間、錄影長度、有魚影像百分比%這幾項，可支援各欄位由大到小或由小到大切換排序，也支援文字搜尋，例如魚種搜尋。選好影片後點選撥放可打開影片進行觀看，我們的後端系統會自動將魚隻百分比與樣本數都太少的影片初步過濾不顯示在前端介面，初步過濾可節省搜尋時間。  
  
<div align=center><img width="466" height="714" src="https://github.com/JGH-GOU/AIFish_Surveillance/blob/main/Demo_PIC/Interface_1.JPG"/></div>  
  
  
也能夠點選「有魚影像百分比(%)」欄位自動由小到大或是由大到小排序，可加快搜尋。  
  
<div align=center><img width="468" height="709" src="https://github.com/JGH-GOU/AIFish_Surveillance/blob/main/Demo_PIC/Interface_4.JPG"/></div>  
  
  
或是輸入魚影像百分比搜尋過濾出超過使用者設定輸入值的影片。  
  
<div align=center><img width="468" height="585" src="https://github.com/JGH-GOU/AIFish_Surveillance/blob/main/Demo_PIC/Interface_5.JPG"/></div>   
  
  
**AI智能魚蹤搜尋標籤與縮時撥放**  
點開十字圓型標記後可看到影片
左方影片為加上智能指標時間軸的原始影片  
右方影片為加上智能指標時間軸且經過AI辨識後的影片  

<div align=center><img width="733" height="747" src="https://github.com/JGH-GOU/AIFish_Surveillance/blob/main/Demo_PIC/Interface_2.JPG"/></div>   

  
點選「智能縮時撥放」按鈕會根據AI所計算有魚活動時間點，自動連續播放只有魚蹤跡的部分(影片進度條中標示紅色表示有魚蹤跡的部分)忽略掉沒有魚的部分(跳掉，影片進度條中標示白色表示沒有魚蹤跡的部分)。  
   
<div align=center><img width="733" height="747" src="https://github.com/JGH-GOU/AIFish_Surveillance/blob/main/Demo_PIC/Interface_3.JPG"/></div>   

  
也可以根據下方表格的排序點選想要觀看的時間點，影片上方會出現您所點選的標籤資訊，同時下方表格也支援各欄位由大到小或由小到大切換排序，包含智能標籤、每個標籤連動的長度、有魚蹤跡百分比(長度佔全影片百分比)等均可自由進行排序。  
   
<div align=center><img width="878" height="502" src="https://github.com/JGH-GOU/AIFish_Surveillance/blob/main/Demo_PIC/Interface_7.jpg"/></div>   
  
  
# 本提案所開發的程式說明

我們所開發的程式分成三部分，可根據開發者需求自由抽換AI模型或是更改網頁介面風格。  

# Part I: 後端AI物件辨識  
請見資料夾: AI_Identify_Program (Inference程式) 
主體語言: python & tensorflow  
環境安裝設置與執行: 請見「環境設定與縮時搜尋程式執行.pdf」(請注意安裝PYTHON套件時須使用一樣的版本)  

以下使我們有使用到的程式與流程：  
標註Label程式下載與操作流程請參考： [LabelImg Github](https://github.com/tzutalin/labelImg)  
訓練程式與指令流程請參考:  
1. [Train FasterRCNN Model](https://github.com/EdjeElectronics/TensorFlow-Object-Detection-API-Tutorial-Train-Multiple-Objects-Windows-10#3-gather-and-label-pictures)  
2. [Train Yolo v4, v3 and v2 for Windows and Linux](https://github.com/AlexeyAB/darknet?fbclid=IwAR1BGCnZhWHPnFdcDcEEzf6LPf0KaPDZ6YyP8a6R1wYiXVCY28irmr5_YNk)  

Inference請見「AI_Identify_Program」資料夾，我們以有提供訓練完成的模型在「inference_graph」資料夾中，可使用「tank」資料夾中的兩隻影片進行測試。 
請執行`object_detection.py`
可選取影片進行推論，而推論的結果將置能標籤位置存在txt檔，可讓Part II的SQL儲存智能標籤資料與AI運算結果、C#抓資料庫數據並連動更新前端介面。若需更換成您自行訓練的模型，請將您的模型置換在「inference_graph」資料夾中。  

# Part II: 前端網頁與AI辨識結果所存之資料庫連結  
請見資料夾: OWBS_WebApp  
主體語言: C# & JavaScript (JavaScript & Visual Studio Tool 開發網頁使用者介面、網頁後端資料庫連結)  
設置環境: Visual Studio 2017 WEB開發套件可開啟專案 & SQL試用版設定後端資料庫  

# Part III: 全自動水下影像除霧技術  
全自動水下影像除霧技術建置於AI-Hub平台，請見[水下影像除霧技術](https://aihub.org.tw/platform/algorithm/4037b4ca-0ab2-11eb-a48e-0242ac120002)連結，利用此方法我們可有效提升水下生物識別的精準度，並與雲端搜尋引擎系統的人工智能模型做整合，其影響效果與API介面如下圖：
  
使用YOLOv5辨識魚群  | mAP@.5 | mAP@.5:.95 
--------------|:-----:|:-----:
**強化前**的辨識率  | 94.2%  |  61.1%
**強化後**的辨識率  | 97.4%  |  72.0%
Difference  | **+3.2%** | **+10.9%**    
  
  
<div align=center><img width="1003" height="744" src="https://github.com/JGH-GOU/AIFish_Surveillance/blob/main/Demo_PIC/Interface_8.JPG"/></div>  
  
  
如果您對此專案有興趣或有任何想法建議，歡迎與我們聯繫：gubycat@gmail.com。  
  
