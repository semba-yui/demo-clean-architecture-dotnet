# ログインAPI

## 機能概要

このAPIは、ユーザーがシステムにログインするためのエンドポイントを提供します。

## リクエスト

- メソッド: POST
- URL: /api/demo-clean-architecture/v1.0/auth/login
- コンテンツタイプ: application/json

### リクエストボディ

```json
{
  "email": "user@example.com",
  "password": "P@ssW0rd"
}
```

| フィールド名 | 型     | 必須 | 説明           |
| ------------ | ------ | ---- | -------------- |
| email        | string | はい | ユーザーのメールアドレス |
| password     | string | はい | ユーザーのパスワード     |

## レスポンス

- ステータスコード: 200 OK
- コンテンツタイプ: application/json

### レスポンスボディ

```json
{
  "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refresh_token": "dGhpcyBpcyBhIHJlZnJlc2ggdG9rZW4uLi4=",
  "is_two_factor_enabled": true
}
```

| フィールド名          | 型     | 説明                           |
| --------------------- | ------ | ------------------------------ |
| access_token          | string | アクセストークン               |
| refresh_token         | string | リフレッシュトークン           |
| is_two_factor_enabled | bool   | 二要素認証が有効かどうか       |

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

- ステータスコード: 401 Unauthorized
  - 説明: 認証エラー
  - レスポンスボディ:
    ```json
    {
      "code": "UNAUTHORIZED",
      "message": "Unauthorized Error"
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
