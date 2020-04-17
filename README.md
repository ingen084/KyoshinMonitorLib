# KyoshinMonitorLib
.NETから簡単に強震モニタを利用できるようにするライブラリ

主に個人用に開発したライブラリです。  
強震モニタを使用したソフトを開発する際に毎回クラスや処理をコピーするのが面倒なので作成しました。

# 更新情報
## 0.3.0.0
### 変更

- 使用ライブラリを更新しました。
- `ParseIntensityFromImage` での解析方法を最適化しました。
- `SecondBasedTimer` の `Accuracy` プロパティを公開しました。

### 破壊的変更

- 各種 `RealTime` を `Realtime` に変更しました。
  - 実際に組んでてストレスマッハだったためです。
- [削除] `LinkedRealtimeData.Intensity`
  - `float?` の拡張メソッドをご利用ください。
- `JmaIntensity.ToShortString` の `Error` `Unknown` 時の表記をそれぞれ `*` `-` に変更しました。
- `ColorToIntensityConverter` のKeyを `int` に変更しました。
  - それに伴い、 `ImageAnalysisResult` の `Color` も `int` に変更されています。

## 0.2.4.5
### 変更

- `AppApi` において休止中の観測点にもマッチングを行うように改善を行いました。

## 0.2.4.4
### 変更

- 依存ライブラリの更新を行いました。
- `MessagePack` の `Lz4BlockArray` に互換があったため、変更しました。

## 0.2.4.3
### 変更

- 依存ライブラリの脆弱性対応を行いました。

## 0.2.4.2
### 変更

- 使用ライブラリを更新しました。
  - `MessagePack` の `Lz4BlockArray` 対応は互換性維持のため一旦見送ります。
- `NtpAssistance` の `GetNetworkTimeWithNtp` の処理をを変更しました。
  - IPアドレスでそのままリクエストを送れるようになりました。

## 0.2.4.1
### バグ修正

- WebAPIのEEWの解析が出来なかったのを修正しました。

## 0.2.4.0
### 変更

- XMLドキュメントを書きました。
- `System.Text.Json` を使用するようにしました。

## 0.2.3.0
### 追加

- nuget.org に公開しました。

## 0.2.2.0
### 変更

- 強震モニタWeb版のURL変更に対応しました。

## 0.2.1.0
### 変更

- .NET Standard2.1(.NET Core3.0)に対応しました。

### 修正

- `WebApi` の `Eew` の `report_time`が正常に解析できていなかった点を修正しました。
- `LinkedObservationPoint` の初期化時に `site` がnullだった場合に例外を投げていた問題を修正しました。

## 0.2.0.2
### 変更

- nugetパッケージを更新しました。

### 削除

- 現在利用できないAPIに `Obsolate` をつけて利用できなくしました。

## 0.2.0.1
### バグ修正

- `GetNetworkTimeWithNtp` の処理を間違えていたのを修正しました。
  - なんで今まで気づかなかったんでしょうかね！


## 0.2.0.0
### 変更

- `SecondBasedTimer`で使用できるプロパティを増やしました。
- AppApiの観測地点のマッチング処理を高速化しました。
  - メモリの消費量もマシになったと思います(ちゃんとはかってない)
- 404などの際に従来の例外を発生させる方式ではなく、`ApiResult`を使用するようにしました。
  - 引き続きタイムアウトでは例外が発生します。
- `LinkedObservationPoint` を追加しました。
  - `ValueTuple`を置き換えただけです。
- `.NET4.6`から`.NET4.6.1`に変更しました。

### 削除

- `FixedTimer` を削除しました。

## 過去の更新情報

- [0.1.x台](https://github.com/ingen084/KyoshinMonitorLib/blob/e581e49192417d9b65a5403681b8507073c66349/README.md#%E6%9B%B4%E6%96%B0%E6%83%85%E5%A0%B1)

# リファレンス
バージョン:`0.2.0.0`  
主要なクラスのみ解説します。詳細な解説はソースなどを参照してください。  
また、気象庁震度階級や地球の緯度経度など、小学生レベルの前提知識が必要なものがあります。

## KyoshinMonitorExceptionクラス
強震モニタのAPIから情報を取得している間に、タイムアウトやレスポンスの異常などが確認された場合に発生する例外です。
### プロパティ
| 型 | 名前 | 解説 |
|---|---|---|
|`string`|Message|どのような例外が発生したか|
|`Exception`|InnerException|内部で発生した例外|

## ApiResultクラス
APIなどを呼んだ際の結果が含まれています。
### プロパティ
| 型 | 名前 | 解説 |
|---|---|---|
|`HttpStatusCode`|StatusCode|HTTPステータスコード|
|`TResult`(ジェネリック)|Data|APIの結果 リクエストに失敗した場合`null`の可能性もあります。|

## ObservationPointクラス
[KyoshinShindoPlaceEditor](https://github.com/ingen084/KyoshinShindoPlaceEditor)と互換があります。
### LoadFromMpk/Json
```c#
public static ObservationPoint[] LoadFromMpk(string path, bool usingLz4 = false);
public static ObservationPoint[] LoadFromJson(string path);
```
観測点情報をmpk/jsonから読み込みます。失敗した場合は例外がスローされます。  
**lz4圧縮済みのmpkを通常のmpkとして読み込まないように注意してください。**

### LoadFromCsv
```c#
public static (ObservationPoint[] points, uint success, uint error) LoadFromCsv(string path, Encoding encoding = null);
```
観測点情報をcsvから読み込みます。失敗した場合は例外がスローされます。

### SaveToCsv/Mpk/Json
```c#
public static void SaveToCsv(string path, IEnumerable<ObservationPoint> points);
public static void SaveToMpk(string path, IEnumerable<ObservationPoint> points, bool useLz4 = false);
public static void SaveToJson(string path, IEnumerable<ObservationPoint> points);
```
拡張メソッド版
```c#
public static void SaveToCsv(this IEnumerable<ObservationPoint> points, string path);
public static void SaveToMpk(this IEnumerable<ObservationPoint> points, string path, bool useLz4 = false);
public static void SaveToJson(this IEnumerable<ObservationPoint> points, string path);
```
観測点情報を各形式に保存します。失敗した場合は例外がスローされます。

## Apiクラス共通
`WebApi`/`AppApi`共通で利用できます。
### プロパティ
| 型 | 名前 | 解説 |
|---|---|---|
|`TimeSpan`|Timeout|APIを呼ぶにあたってのタイムアウト時間|

## WebApiクラス
Webで見ることができる強震モニタのAPIを使用してEEWなどの画像やデータを取得するためのクラスです。
### メソッド
| 返り値の型 | 名前(引数) | 解説 |
|---|---|---|
|`Task<ApiResult<Eew>>`|GetEewInfo(`DateTime` time)|緊急地震速報のJsonを取得します。  EewクラスはJsonをそのままパースしたものです。 |
|`Task<ApiResult<byte[]>>`|GetRealtimeImageData(`DateTime` time, `RealtimeDataType` dataType, `bool` isBehore = false)|リアルタイムな情報(リアルタイム･震度･加速度など)の画像のbyte配列を取得します。  画像解析まで行いたい場合は下記の拡張メソッドをご利用ください。|
|`Task<ApiResult<byte[]>>`|GetEstShindoImageData(`DateTime` time)|緊急地震速報の予想震度の画像のbyte配列を取得します。|
|`Task<ApiResult<byte[]>>`|GetPSWaveImageData(`DateTime` time)|緊急地震速報のP波とS波の広がりを示す円の画像のbyte配列を取得します。|

### KyoshinMonitorLib.Imagesによる拡張メソッド
| 返り値の型 | 名前(引数) | 解説 |
|---|---|---|
|`Task<ApiResult<IEnumerable<ImageAnalysisResult>>>`|ParseIntensityFromParameterAsync(this `WebApi` webApi, `IEnumerable<ObservationPoint>` points, `DateTime` datetime, `bool` isBehole = false)|ObservationPointのコレクションを使用して新強震モニタリアルタイム震度の画像を取得し、解析します。|

他にもありますが割愛させていただきます。

#### 画像から震度を解析するにあたってのメモ

(生の)震度7.0以上が観測された場合、必ず7.0が帰ってくるようです。  
`7.0+` のような表記を採用してあげるとユーザーに優しいかもしれません。  
尚、AppApiで取得した値は7.0以上でも正確に表示されます。

## AppApiクラス
スマートフォンアプリケーションのAPIを使用してリアルタイム震度などのデータを取得します。  
**ほとんどのAPIが現在利用できません。** いつか復活を願って処理は残しておきます…。
### メソッド
| 返り値の型 | 名前(引数) | 解説 |
|---|---|---|
|`Task<ApiResult<LinkedRealtimeData[]>>`|GetLinkedRealtimeData(`DateTime` time, `RealtimeDataType` dataType, `bool` isBehore = false)|リアルタイムデータを取得します。  自動で観測点情報などと結びつけ、インスタンスを返します。|
|`Task<ApiResult<RealtimeData>>`|GetRealtimeData(`DateTime` time, `RealtimeDataType` dataType, `bool` isBehore = false)|リアルタイムデータを取得します。  特に理由がない限り`GetLinkedRealtimeData`を使用することを推奨します。|
|`Task<ApiResult<SiteList>>`|GetSiteList(`string` baseSerialNo)|APIから参照できる観測点情報の一覧を取得します。  特に理由がない限り`GetLinkedRealtimeData`を使用することを推奨します。|
|`Task<ApiResult<Hypo>>`|GetEewHypoInfo(`DateTime` time)|**[利用不可]** APIから緊急地震速報の情報を取得します。  **ちなみに、複数のEEWに対応してそうです…(要検証)**|
|`Task<ApiResult<PSWave>>`|GetPSWave(`DateTime` time)|**[利用不可]** 緊急地震速報から算出された揺れの広がりを取得します。  **こちらも複数のEEWに対応してそうです。**|
|`Task<ApiResult<EstShindo>>`|GetEstShindo(`DateTime` time)|**[利用不可]** 緊急地震速報から算出された予想震度の5kmメッシュ情報を取得します。|
|`Task<ApiResult<Mesh[]>>`|GetMeshes()|**[利用不可]** メッシュ一覧を取得します。 非常に時間がかかるため、起動時などに行い、別ファイルとしてキャッシュしておくことを推奨します。|

### 重要事項
- `GetEewHypoInfo`
- `GetPSWave`
- `GetEstShindo`

**この3つのAPIはEEWが発表されていない場合は404が帰ってきます。**

## Meshクラス
5kmメッシュ情報を取り扱います。
### プロパティ
| 型 | 名前(引数) | 解説 |
|---|---|---|
|`string`|Code|メッシュのコード 詳細不明|
|`Location`|LocationLeftTop|右上(北西)の緯度経度|
|`Location`|LocationRightBottom|左下(南東)の緯度経度|

### 備考
使用方法の詳細は省略しますが、 `GetEstShindo` の返り値を見ればわかると思います。  
ですが需要があれば書くかもしれません。またお知らせください。


## UrlGeneratorクラス群
UrlGeneratorは分離した上に、各種Apiクラスでラップしているため、解説は省略させていただきます。

## SecondBasedTimerクラス
FixedTimerに時刻管理機能をつけたものです。  
強震モニタの取得タイマーとしてしか考慮していないので必ず1秒になります。

### 注意
時間の更新(補正)は自動でされないため、別途タイマーなどで実行してください。

### サンプル
```c#
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
timer.Start(await NtpAssistance.GetNetworkTimeWithNtp() ?? throw new Exception());

// 時刻の補正(別のタイマーとかで回すといいと思います)
//timer.UpdateTime(await NtpAssistance.GetNetworkTimeWithNtp() ?? throw new Exception());

//改行入力待ち
Console.ReadLine();
//タイマーストップ
timer.Stop();
```

## NtpAssistance
NTPから簡単に時刻取得をするクラスです。

### メソッド
| 返り値の型 | 名前(引数) | 解説 |
|---|---|---|
|`Task<DateTime?>`|GetNetworkTimeWithNtp(`string` hostName = "ntp.nict.jp", `ushort` port = 123, `int` timeout = 100)|SNTP通信を使用してネットワーク上から時刻を取得します。  一応SNTPを実装していますが、NICT以外のNTPサーバーでの挙動は保証しません。|
|`Task<DateTime?>`|GetNetworkTimeWithHttp(`string` url = "https://ntp-a1.nict.go.jp/cgi-bin/jst", `double` timeout = 1000)|Http通信を使用してネットワーク上から時刻を取得します。  小数のPOSIX Timeを含んだレスポンスが返されるURLであればなんでも使用できるとおもいます。|

## JmaIntensity
気象庁震度階級を示す列挙型です。震度異常などを扱うために値が増やされています。

### サンプル
```c#
JmaIntensity shindo = 1.0f.ToJmaIntensity(); //JmaIntensity.Int1
Console.WriteLine(shindo.ToShortString()); //1
Console.WriteLine(shondo.ToLongString()); //震度1

Console.WriteLine("5+".ToJmaIntensity().ToLongString()); //文字からも解析できます。 出力:震度5強

float? invalidIntensity = null;
Console.WriteLine(invalidIntensity.ToJmaIntensity()); //nullableなfloatもできます。 出力:JmaIntensity.Unknown
```
