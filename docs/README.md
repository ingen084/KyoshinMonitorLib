# 概要
主に個人用に開発したライブラリです。  
強震モニタを使用したソフトを開発する際に毎回クラスや処理をコピーするのが面倒なので作成しました。

# 更新情報
## 0.0.6.2
### 修正
- `UrlType`の`EstShindo`が`RestShindo`になっていたのを修正しました。  
  尚、`RestShindo`はObsoleteになっていますので、利用されている方は修正をお願いします。

## 0.0.6.1
### 修正
- シリアライズに失敗する可能性があった部分を修正しました。

## 0.0.6.0
### 削除
- `NtpTimer`を削除しました。

### 追加
- `NtpTimer`に代わり、`SecondBasedTimer`を追加しました。  
  - 間隔が設定できなくなりました。常に毎秒イベントが発生します。
  - 時刻取得機能を廃止し、`UpdateTime`メソッドによる手動での時刻更新が利用できるようにしました。

### 変更
- `NtpAssistance`の処理を変更し、通信遅延の考慮もするようにしました。

## 0.0.5.0
### 追加
- `FixedTimer`･`NtpTimer`にプロパティ`BlockingMode`を追加しました。  
  Trueにすると前回のイベントの実行中であれば新たにイベントが発行されなくなります。  
  初期値はTrueになっています。

### 修正
- `FixedTimer`･`NtpTimer`の挙動を修正し、一度に大量のイベントが発生しないようにしました。

## 0.0.4.1
### 修正
- `IEnumerable<ObservationPoint>`の拡張メソッド`ParseIntensityFromParameterAsync`において遅延評価を考慮せずに処理していたため正常に色が震度に変換されない場合がある不具合を修正しました。

## 0.0.4.0
### 追加
- UWP版の提供を開始しました。  
  が、注意事項があります、下記参照してください。
- `ImageAnalysisResult`に解析に使用した色を保存するプロパティ`Color`を追加しました。
- `IEnumerable<ImageAnalysisResult>`の拡張メソッドを使用して一括で画像から震度に解析する際に新しくインスタンスを生成せずにインスタンスを使いまわすようにしました。  
  これによって負荷・GC回数の軽減を期待しています。
- NTPベースのタイマー`NtpTimer`を作成しました。

## 0.0.3.0
### 追加
- 気象庁震度階級を示す列挙型 `JmaIntensity` の追加(使用方法はリファレンス参照)
- XMLドキュメントの追加

## 0.0.2.0
### 追加
- NTP補助クラス
- 観測点情報で新たに
  - Json
  - MessagePack
  - MessagePack+LZ4

  のサポート

### 仕様変更
- FixedTimer  
  時間のベースにDateTime.Nowを使用していたものをQueryPerformanceCounterに変更  
  そのためDateTime.Nowで確認すると精度が低下したようにみえるかもしれません。

## 0.0.1.0
初版

# UWP版について
ProtoBufについては非対応です。

# リファレンス
バージョン:`0.0.6.0`  
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
```cs
//観測点情報読み込み
var points = ObservationPoint.LoadFromMpk("ShindoObsPoints.mpk.lz4", true);
//時間計算(今回は適当にPC時間-5秒)
var time = DateTime.Now.AddSeconds(-5);
//画像を取得して結果を計算
IEnumerable<ImageAnalysisResult> result = await points.ParseIntensityFromParameterAsync(time, false);
```
#### 注釈
`ImageAnalysisResult`は`ObservationPoint`を継承したクラスになっていて、そのメンバの`AnalysisResult`に震度が入っています。  
`IsSuspended`がtrueの場合や、震度に変換できなかった場合、ピクセル取得に例外が発生した場合はnullが代入されています。

### ParseIntensityFromBitmap
```cs
public static IEnumerable<ImageAnalysisResult> ParseIntensityFromImage(this IEnumerable<ObservationPoint> obsPoints, Bitmap bitmap);
```
与えられた画像から観測点情報を使用し震度を取得します。
#### サンプル
Bitmapを指定するだけでFromParameterAsyncと何ら変わりはないので省略

## ColorToIntensityConverter
### Convert
```cs
public static float? Convert(System.Drawing.Color color);
```

色を震度に変換します。テーブルにない値を参照した場合nullが返されます。  
透明度も判定されるので十分気をつけてください。
#### サンプル
```cs
//using System.Drawing;
Color color = Color.FromArgb(255, 63, 250, 54); //とりあえずサンプル色を作成
float? result = ColorToIntensityConverter.Convert(color); //0
```

## ObservationPoint
[KyoshinShindoPlaceEditor](https://github.com/ingen084/KyoshinShindoPlaceEditor)と互換があります。
### LoadFromPbf/Mpk/Json
```cs
public static ObservationPoint[] LoadFromPbf(string path);
public static ObservationPoint[] LoadFromMpk(string path, bool usingLz4 = false);
public static ObservationPoint[] LoadFromJson(string path);
```
観測点情報をpbf/mpk/jsonから読み込みます。失敗した場合は例外がスローされます。  
**lz4圧縮済みのmpkを通常のmpkとして読み込まないように注意してください。**

### LoadFromCsv
```cs
public static (ObservationPoint[] points, uint success, uint error) LoadFromCsv(string path, Encoding encoding = null);
```
観測点情報をcsvから読み込みます。失敗した場合は例外がスローされます。

### SaveToPbf/Csv/Mpk/Json
```cs
public static void SaveToPbf(string path, IEnumerable<ObservationPoint> points);
public static void SaveToCsv(string path, IEnumerable<ObservationPoint> points);
public static void SaveToMpk(string path, IEnumerable<ObservationPoint> points, bool usingLz4 = false);
public static void SaveToJson(string path, IEnumerable<ObservationPoint> points);
```
拡張メソッド版
```cs
public static void SaveToPbf(this IEnumerable<ObservationPoint> points, string path);
public static void SaveToCsv(this IEnumerable<ObservationPoint> points, string path);
public static void SaveToMpk(this IEnumerable<ObservationPoint> points, string path, bool usingLz4 = false);
public static void SaveToJson(this IEnumerable<ObservationPoint> points, string path);
```
観測点情報を各形式に保存します。失敗した場合は例外がスローされます。

## UrlGenerator
### Generate
```cs
public static string Generate(UrlType urlType, DateTime datetime,
	RealTimeImgType realTimeShindoType = RealTimeImgType.Shindo, bool isBerehole = false);
```
与えられた値を使用して**新**強震モニタのURLを生成します。
#### サンプル
```cs
DateTime time = DateTime.Parse("2017/03/19 22:13:47");
string url = UrlGenerator.Generate(UrlType.EewJson, time); //http://www.kmoni.bosai.go.jp/new/webservice/hypo/eew/20170319221347.json
string url2 = UrlGenerator.Generate(UrlType.RealTimeImg, time, RealTimeImgType.Shindo, true); //http://www.kmoni.bosai.go.jp/new/data/map_img/RealTimeImg/jma_b/20170319/20170319221347.jma_b.gif
```

## FixedTimer
通常のタイマー(System.Timers.Timerなど。FormsのTimerは申し訳ないが論外)では、誤差が蓄積していきずれていきますが、それを対策したものです。

### サンプル
```cs
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

## SecondBasedTimer
FixedTimerにNTPからの自動取得機能・オフセットの設定機能をつけたものです。  
強震モニタの取得タイマーとしてしか考慮していないので必ず1秒になります。

### 注意
時間の更新(補正)は自動でされないため、別途タイマーなどで実行してください。

### サンプル
```cs
//タイマーのインスタンスを作成(デフォルトは精度1ms↓)
var timer = new SecondBasedTimer()
{
	Offset = TimeSpan.FromSeconds(2.5), //イベントの発火時間を2500ms *後ろに* ずらす。だいたいこれ前後がおすすめ。
};
//適当にイベント設定
timer.Elapsed += time =>
{
	//timeに時間が入っているのでそれを使用して取得する
};
//タイマー開始 引数には現在の時刻が必要です。
await timer.Start(await NtpAssistance.GetNetworkTimeWithNtp());
//改行入力待ち
Console.ReadLine();
//タイマーストップ
timer.Stop();
```

## NtpAssistance
NTPから簡単に時刻取得をするクラスです。

### GetNetworkTimeWithNtp
```cs
public static async Task<DateTime> GetNetworkTimeWithNtp(string hostName = "ntp.nict.jp", ushort port = 123, int timeout = 100);
```
Ntp通信を使用してネットワーク上から時刻を取得します。  
プロトコル実装が適当なのでNICT以外のNTPサーバーでの挙動は保証しません。

#### サンプル
```cs
//timeがもう時間
 var time = await NtpAssistance.GetNetworkTimeWithNtp();
```
### GetNetworkTimeWhithHttpAsync
```cs
public static async Task<DateTime> GetNetworkTimeWhithHttpAsync(string url = "http://ntp-a1.nict.go.jp/cgi-bin/ntp", double timeout = 100);
```
Http通信を使用してネットワーク上から時刻を取得します。**未検証です。(ぉぃ)**  
可能な限りNTPを使用することを推奨します。  
NTPの時刻が生で返されるURLである必要があります。  
**注意 NICTのサーバーはこのURLだけではありません。**

#### サンプル
```cs
//timeがもう時間
 var time = await NtpAssistance.GetNetworkTimeWhithHttpAsync();
```

## JmaIntensity
気象庁震度階級を示す列挙型(Enum)です。震度異常などを扱うために値が増やされています。

### サンプル
```cs
JmaIntensity shindo = 1.0f.ToJmaIntensity(); //JmaIntensity.Int1
Console.WriteLine(shindo.ToShortString()); //1
Console.WriteLine(shondo.ToLongString()); //震度1

Console.WriteLine("5+".ToJmaIntensity().ToLongString()); //文字からも解析できます。 出力:震度5強

float? invalidIntensity = null;
Console.WriteLine(invalidIntensity.ToJmaIntensity()); //nullableなfloatもできます。 出力:JmaIntensity.Unknown
```
