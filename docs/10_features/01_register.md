# ユーザー登録API

## 機能概要

このAPIは、新しいユーザーをシステムに登録するためのエンドポイントを提供します。

## リクエスト

- メソッド: POST
- URL: /api/demo-clean-architecture/v1.0/auth/register
- コンテンツタイプ: application/json

### リクエストボディ

```json
{
  "user_name": "test-user",
  "email": "user@example.com",
  "password": "P@ssW0rd"
}
```

| フィールド名 | 型     | 必須 | 説明           |
| ------------ | ------ | ---- | -------------- |
| user_name    | string | はい | ユーザーの名前 |
| email        | string | はい | ユーザーのメールアドレス |
| password     | string | はい | ユーザーのパスワード     |

## レスポンス

- ステータスコード: 200 OK
- コンテンツタイプ: application/json

### レスポンスボディ

```json
{
  "user_id": 1
}
```

| フィールド名 | 型     | 説明           |
| ------------ | ------ | -------------- |
| user_id      | int    | 登録されたユーザーのID |

## エラーハンドリング

- ステータスコード: 400 Bad Request
  - 説明: リクエストボディのバリデーションエラー
  - レスポンスボディ:
    ```json
    {
      "code": "INVALID_PARAMETER",
      "message": "Invalid parameter"
    }
    ```

- ステータスコード: 500 Internal Server Error
  - 説明: サーバー内部エラー
  - レスポンスボディ:
    ```json
    {
      "code": "INTERNAL_ERROR",
      "message": "Internal Server Error"
    }
    ```
