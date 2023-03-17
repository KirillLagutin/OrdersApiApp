## Техническое задание:
## [API Заказы](https://ordersapiapp-kirilllagutin.amvera.io/)".
БД содержит 4 таблицы:
1) Товар - содержит id, наименование товар, артикул (число) - 
2) Заказ - содержит id, id клиента (связь), описание заказа (description)
3) Расшивка заказ-товар - содержит id, id товара (связь), id заказа (связь), кол-во единиц товара
4) Клиент - содержит id, имя клиента

-----
Необходимо реализовать Web API для CRUD-операций в указанной модели данных.
CRUD - create read update delete

ЗАДАНИЕ
1. Реализовать базовый CRUD в виде API для всех сущностей
2. Реализовать возможность получения полной информации о заказе со списком товаров и кол-ве товаров в нем
3. Реализовать запрос "Чек", который пречисляет товары в заказе и суммирует их стоимость в общую сумму
4. Реализовать возможность удаления заказа вместе со всеми записями расшивки и клиента (
	цепочка удаления должна работать при удалении любой из записей вышепеерчисленных таблиц
)

Требования:
1. Использование принципов DI, API, REST, SOLID
2. Работа с git
3. Красивый, понятный, читаемые, комментируемый код.
----------------------------------------------------------------------------------------------------------

## Клонируем, запускаем и тестим в постмане
