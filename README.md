# demo-clean-architecture-dotnet

## プロジェクト概要

このプロジェクトは、認証認可機能を持つ基本的なデモAPIを提供します。以下の主要な機能を含みます。

### 主な機能

* **認証と認可**: ユーザー登録、ログイン、ログアウト、二要素認証などの認証機能を提供します。
* **ロールと権限管理**: ロールと権限の作成、更新、削除、取得機能を提供します。
* **ユーザー管理**: ユーザーの詳細取得、削除機能を提供します。
* **エラーハンドリング**: カスタムバリデーションフィルターとグローバル例外フィルターを使用したエラーハンドリング機能を提供します。
* **ヘルスチェック**: アプリケーションのヘルスチェックエンドポイントを提供します。
* **コードフォーマットとマイグレーション**: コードフォーマットとデータベースマイグレーションの手順を提供します。
* **OpenAPIサポート**: APIドキュメントのためのOpenAPIサポートを提供します。
* **ロギング**: Serilogを使用したロギング機能を提供します。

## 前提条件

このプロジェクトを実行するために必要なソフトウェアとツールは以下の通りです。

* .NET SDK
* Docker

## インストール

リポジトリをクローンし、プロジェクトをローカルにセットアップする手順は以下の通りです。

```shell
git clone https://github.com/semba-yui/demo-clean-architecture-dotnet.git
cd demo-clean-architecture-dotnet
```

## 使用方法

プロジェクトを実行する手順と主要なコンポーネントとの対話方法は以下の通りです。

```shell
dotnet run --project ./src/DemoCleanArchitecture.Api
```

## Migration

### Create migration

```shell
dotnet tool run dotnet-ef migrations add InitialIdentity \
  --project ./src/DemoCleanArchitecture.Infrastructure \
  --startup-project ./src/DemoCleanArchitecture.Api \
  --context DemoCleanArchitectureDbContext
```

```shell
dotnet tool run dotnet-ef migrations add InitialData \
  --project ./src/DemoCleanArchitecture.Infrastructure \
  --startup-project ./src/DemoCleanArchitecture.Api \
  --context DemoCleanArchitectureDbContext
```

### Update database

```shell
dotnet tool run dotnet-ef database update InitialIdentity \
  --project ./src/DemoCleanArchitecture.Infrastructure \
  --startup-project ./src/DemoCleanArchitecture.Api \
  --context DemoCleanArchitectureDbContext
```

```shell
dotnet tool run dotnet-ef database update InitialData \
  --project ./src/DemoCleanArchitecture.Infrastructure \
  --startup-project ./src/DemoCleanArchitecture.Api \
  --context DemoCleanArchitectureDbContext
```

### Remove migration

```shell
dotnet tool run dotnet-ef migrations remove \
  --project ./src/DemoCleanArchitecture.Infrastructure \
  --startup-project ./src/DemoCleanArchitecture.Api \
  --context DemoCleanArchitectureDbContext
```

## コードフォーマット

```shell
dotnet tool run jb cleanupcode DemoCleanArchitecture.sln
```

## テスト

テストを実行する手順と使用する特定のテストツールやフレームワークについて説明します。

## コントリビューティング

プロジェクトに貢献するためのガイドライン、問題やプルリクエストの提出方法について説明します。

## ライセンス

このプロジェクトのライセンス情報と`LICENSE`ファイルへのリンクを記載します。

## 謝辞

サードパーティのライブラリ、ツール、または貢献者へのクレジットを記載します。

## 連絡先情報

サポートや質問のためのメンテナーまたは貢献者への連絡方法を記載します。

## 備考

乱数生成（JWTのシークレットキー）

```shell
$ openssl rand -base64 32
zT13v36WiWNVzW/XrhaKGW/e78a/1hsNy0h2DdqRmYo=
```
