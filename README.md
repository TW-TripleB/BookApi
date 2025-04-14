一個使用 .NET 9 和 MongoDB 打造的 RESTful API，提供書籍資料的 CRUD 功能，適合用於學習、練習或作為中小型專案的後端基礎。

✨ 功能特色
📖 提供書籍的新增、查詢、更新與刪除（CRUD）功能

🧩 採用 ASP.NET Core 架構，結構清晰

🗃️ 使用 MongoDB 作為資料庫，透過 MongoDbService 管理資料存取

🔧 支援 Docker Compose，方便部署與測試

🔍 提供 BookApi.http 檔案，可透過 Visual Studio 或 VS Code 測試 API

📦 技術架構
語言：C# (.NET 9)

資料庫：MongoDB

架構：RESTful API

部署：Docker Compose

目錄結構：

Controllers/：API 控制器

Models/：資料模型

Service/：MongoDB 資料存取服務

Properties/：應用程式設定

appsettings.json：環境與資料庫設定

docker-compose.yml：容器化設定
