# 概要
主に個人用に開発したライブラリです。  
強震モニタを使用したソフトを開発する際に毎回クラスや処理をコピーするのが面倒なので作成しました。

# リファレンス
バージョン:`0.0.1.0`  
一部の解説のみ行います。各メソッドのパラメータはコメントを参照してください。

## 一番知ってほしい機能
簡単に強震モニタの震度を取得できる機能を提供します。
### ParseIntensityFromParameterAsync
```c#
public static async Task<ImageAnalysisResult[]> ParseIntensityFromParameterAsync(this IEnumerable<ObservationPoint> points, DateTime datetime, bool isBehole = false);
```
与えられた情報から強震モニタの画像を取得し、そこから観測点情報を使用し震度を解析します。  
asyncなのは画像取得部分のみなので注意してください。  
ちなみに、画像が取得できないなどの場合は容赦なく例外を吐くので注意してください。

#### サンプル
```c#
//観測点情報読み込み
var points = ObservationPoint.LoadFromPbf("ShindoObsPoints.pbf");
//時間計算(今回は適当にPC時間-5秒)
var time = DateTime.Now.AddSeconds(-5);
//画像を取得して結果を計算
ImageAnalysisResult[] result = await points.ParseIntensityFromParameterAsync(time, false);
```
#### 注釈
`ImageAnalysisResult`は`ObservationPoint`を継承したクラスになっていて、そのメンバの`AnalysisResult`に震度が入っています。  
`IsSuspended`がtrueの場合や、震度に変換できなかった場合、ピクセル取得に例外が発生した場合はnullが代入されています。

### ParseIntensityFromBitmap
```c#
public static ImageAnalysisResult[] ParseIntensityFromImage(this IEnumerable<ObservationPoint> obsPoints, Bitmap bitmap);
```
与えられた画像から観測点情報を使用し震度を取得します。
#### サンプル
Bitmapを指定するだけでFromParameterAsyncと何ら変わりはないので省略

## ColorToIntensityConverter
### Convert
```c#
public static float? Convert(System.Drawing.Color color);
```

色を震度に変換します。テーブルにない値を参照した場合nullが返されます。  
透明度も判定されるので十分気をつけてください。
#### サンプル
```c#
//using System.Drawing;
Color color = Color.FromArgb(255, 63, 250, 54); //とりあえずサンプル色を作成
float? result = ColorToIntensityConverter.Convert(color); //0
```

## ObservationPoint
[KyoshinShindoPlaceEditor](https://github.com/ingen084/KyoshinShindoPlaceEditor)と互換があります。
### LoadFromPbf
```c#
public static ObservationPoint[] LoadFromPbf(string path);
```
観測点情報をpbfから読み込みます。失敗した場合は例外がスローされます。

### LoadFromCsv
```c#
public static (ObservationPoint[] points, uint success, uint error) LoadFromCsv(string path, Encoding encoding = null);
```
観測点情報をcsvから読み込みます。失敗した場合は例外がスローされます。

### SaveToPbf/Csv
```c#
public static void SaveToPbf(string path, IEnumerable<ObservationPoint> points);
public static void SaveToCsv(string path, IEnumerable<ObservationPoint> points);
```
拡張メソッド版
```c#
public static void SaveToPbf(this IEnumerable<ObservationPoint> points, string path);
public static void SaveToCsv(this IEnumerable<ObservationPoint> points, string path);
```
観測点情報をpbf/csvに保存します。失敗した場合は例外がスローされます。

## UrlGenerator
### Generate
```c#
public static string Generate(UrlType urlType, DateTime datetime,
	RealTimeImgType realTimeShindoType = RealTimeImgType.Shindo, bool isBerehole = false);
```
与えられた値を使用して**新**強震モニタのURLを生成します。
#### サンプル
```c#
DateTime time = DateTime.Parse("2017/03/19 22:13:47");
string url = UrlGenerator.Generate(UrlType.EewJson, time); //http://www.kmoni.bosai.go.jp/new/webservice/hypo/eew/20170319221347.json
string url2 = UrlGenerator.Generate(UrlType.RealTimeImg, time, RealTimeImgType.Shindo, true); //http://www.kmoni.bosai.go.jp/new/data/map_img/RealTimeImg/jma_b/20170319/20170319221347.jma_b.gif
```

## FixedTimer
通常のタイマー(System.Timers.Timerなど。FormsのTimerは申し訳ないが論外)では、誤差が蓄積していきずれていきますが、それを対策したものです。

### サンプル
```c#
//タイマーのインスタンスを作成(デフォルトは間隔1000ms+精度1ms↓)
var timer = new FixedTimer()
{
	Interval = TimeSpan.FromMilliseconds(500), //500msに設定
};
//適当にイベント設定
timer.Elapsed += () =>
{
	//適当な処理
};
//タイマー開始
timer.Start();
//改行入力待ち
Console.ReadLine();
//タイマーストップ
timer.Stop();
```