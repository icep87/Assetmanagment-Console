# MiniProject-Console

### Inventory managment 

Build with DDD structure in mind the project is divided in 3 parts. 

1. UI
2. Data
3. Domain

Following options are available:

1. Inventory - **FULL CRUD**
2. Offices - **FULL CRUD**
3. Currencies - Read, Create, Update
4. Reports:
  * Inventory near end of warraty
  * Inventory per office
5. Categories - Read, Create,


To configure the Connection string do following:
- Add  appsettings.json to the MiniProject-Console.Data project folder with the following information: 
```json
{
  "ConnectionStrings": {
    "InventoryDatabase": "Data Source = SOURCE; Initial Catalog = CATALOG ; User Id = USER; Password = PASSWORD;"
  }
}
```
#### Run migrations to setup database
