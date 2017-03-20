# 概要
主に個人用に開発したライブラリです。  
強震モニタを使用したソフトを開発する際に毎回クラスや処理をコピーするのが面倒なので作成しました。

# リファレンス
一部の解説のみ行います。各メソッドのパラメータはコメントを参照してください。
## ColorToIntensityConverter
### Convert
```c#
public static float? Convert(System.Drawing.Color color)
```

色を震度に変換します。テーブルにない値を参照した場合nullが返されます。  
透明度も判定されるので十分気をつけてください。
#### サンプル
```c#
//using System.Drawing;
//using KyoshinMonitorLib;
Color color = Color.FromArgb(255, 63, 250, 54); //とりあえずサンプル色を作成
float? result = ColorToIntensityConverter.Convert(color); //0
```

## ObservationPoint
[KyoshinShindoPlaceEditor](https://github.com/ingen084/KyoshinShindoPlaceEditor)と互換があります。
### LoadFromPbf
```C#
public static ObservationPoint[] LoadFromPbf(string path)
```
観測点情報をpbfから読み込みます。失敗した場合は例外がスローされます。

### LoadFromCsv
```C#
public static (ObservationPoint[] points, uint success, uint error) LoadFromCsv(string path, Encoding encoding = null)
```
観測点情報をcsvから読み込みます。失敗した場合は例外がスローされます。

### SaveToPbf/Csv
```c#
public static void SaveToPbf(string path, IEnumerable<ObservationPoint> points)
public static void SaveToCsv(string path, IEnumerable<ObservationPoint> points)
```
拡張メソッド版
```c#
public static void SaveToPbf(this IEnumerable<ObservationPoint> points, string path)
public static void SaveToCsv(this IEnumerable<ObservationPoint> points, string path)
```
観測点情報をpbf/csvに保存します。失敗した場合は例外がスローされます。

## UrlGenerator
### Generate
```c#
public static string Generate(UrlType urlType, DateTime datetime,
	RealTimeImgType realTimeShindoType = RealTimeImgType.Shindo, bool isBerehole = false)
```
与えられた値を使用して強震モニタのURLを生成します。
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