		drop database if exists MPC;

create database MPC;

use MPC;
	
create table Account(
	account_id int  primary key,
	username nvarchar(100) not null,
    password nvarchar(100) not null,
    staffname nvarchar(100) not null
    
);


create table Items(
	item_id int  primary key, 
    item_name nvarchar(200) ,
    item_price decimal(20,2) default 0   , 
    amount int not null default 0,
    item_status tinyint not null default 1 /*0:hết sp, 1:chưa hết sp */
   
);
create table Tables
( table_id int  primary key,
table_name nvarchar(100) not null,
table_status tinyint not null default 0 /*0: trống, 1: đã có người*/
);
create table Orders(
	order_id int auto_increment primary key ,
    order_date datetime default now(),
    order_status tinyint not null default 0, /*0: chưa thanh toán, 1: đã thanh toán, 2: hủy order*/
    account_id int not null,
    table_id int not null,
    constraint fk_Orders_Account foreign key(account_id) references Account(account_id),
    constraint fk_Orders_Tables foreign key(table_id) references Tables(table_id)
);

create table OrderDetails(
	order_id int not null,
    item_id int  not null,
    item_price decimal(20,2) not null ,
    quantity int not null,
    constraint pk_OrderDetails primary key(order_id,item_id),
    constraint fk_OrderDetails_Orders foreign key(order_id) references Orders(order_id),
    constraint fk_OrderDetails_Items foreign key(item_id) references Items(item_id)
    
);



/*insert data */
insert into Account(account_id,username,password ,staffname ) values 
(1,'staff1','123456','An'),
(2,'staff2','123456','Phương'),
(3,'staff3','123456','Quân'),
(4,'staff4','123456','Linh'),
(5,'staff5','123456','Quyên'),
(6,'staff6','123456','Quỳnh'),
(7,'staff7','123456','Ngọc'),
(8,'staff8','123456','Nhi'),
(9,'staff9','123456','Bảo');
select * from Account;



insert into Items(item_id,item_price, amount,item_name) values
	(1,20.000, 100,'khoai tây chiên'),
    (2,15.000, 100,'hướng dương'),
    (3,20.000, 100,'bánh ngọt'),
    (4,45.000, 100,'mỳ ý'),
    (5,35.000, 100,'hamburger'),
    (6,8.000, 100,'xúc xích'),
    (7,25.000, 100,'sữa chua đanh đá'),
    (8,20.000, 100,'hoa quả dầm'),
    (9,35.000, 100,'cafe sữa đánóng'),
    (10,35.000, 100,'mojito bạc hà'),
    (11,35.000, 100,'mojito việt quất'),
    (12,35.000, 100,'mojito dâu '),
    (13,35.000, 100,'mojito chanh'),
	(14,35.000, 100,'mojito xoài'),
	(15,35.000, 100,'mojito kiwi'),
	(16,7.000, 100,'trà nhài'),
    (17,23.000, 100,'trà đào'),
    (18,10.000, 100,'trà ô long'),
	(19,55.000, 100,'capuchino'),
	(20,55.000, 100,'caramel'),
	(21,45.000, 100,'mocha'), 
	(22,10.000, 100,'coca'),
    (23,10.000, 100,'pepsi'),
    (24,10.000, 100,'sting');
select * from Items;

insert into Tables( table_id,table_name,table_status) values

(1,'bàn 1',0),
(2,'bàn 2',0),
(3,'bàn 3',0),
(4,'bàn 4',0),
(5,'bàn 5',0),
(6,'bàn 6',0),
(7,'bàn 7',0),
(8,'bàn 8',0),
(9,'bàn 9',0),
(10,'bàn 10',0),
(11,'bàn 11',0),
(12,'bàn 12',0),
(13,'bàn 13',0),
(14,'bàn 14',0),
(15,'bàn 15',0),
(16,'bàn 16',0),
(17,'bàn 17',0),
(18,'bàn 18',0),
(19,'bàn 19',0),
(20,'bàn 20',0),
(21,'bàn 21',0),
(22,'bàn 22',0),
(23,'bàn 23',0),
(24,'bàn 24',0),
(25,'bàn 25',0),
(26,'bàn 26',0),
(27,'bàn 27',0),
(28,'bàn 28',0),
(29,'bàn 29',0),
(30,'bàn 30',0),
(31,'bàn 31',0),
(32,'bàn 32',0),
(33,'bàn 33',0),
(34,'bàn 34',0),
(35,'bàn 35',0),
(36,'bàn 36',0);
select * from Tables;
select *from OrderDetails; 
/*insert into OrderDetails( item_id ,item_price  ,quantity ) values( 1, 8,9),(1,9,8);*/
select *from Orders;
/*SELECT DISTINCT  
   item_id,
   item_price,
   SUM(quantity) AS quantity
FROM OrderDetails
GROUP BY item_id , item_price, quantity;
select last_insert_id() as order_id;
select * from Orders as o inner join OrderDetails as od on o.order_id = od.order_id  where o.order_id= 1;
/*delimiter $$
create procedure sp_EditOrder(IN orderid int , in item_id ,item_price decimal(20,2) not null ,quantity int not null,)
begin
if()
	insert into Customers(customer_name, customer_address) values (customerName, customerAddress); 
    select max(customer_id) into customerId from Customers;
ele
end $$
delimiter ;
declare @dem int 
select @dem=count(*) from Customer where value=@value
if(@dem>0) begin update Customer set years=years+@years where value=@value end
else begin insert ... end $$
delimiter ;
 /*declare @dem int 
 
select @dem=count(*) from Customer where value=@value 
if(@dem>0) begin update Customer set years=years+@years where value=@value end
else begin insert ... end
INSERT INTO OrdersDetails (order_id, item_id, unit_price,quantity) VALUES(1, 4,20.000,16)  ON DUPLICATE KEY UPDATE item_id =3 , quantity=5;
insert into OrdersDetails(item_id,item_price,quantity) values(3,20.000,10);
INSERT INTO Orders ( item_id, item_price, opted_in) 
  SELECT id, name, username, opted_in 
  FROM user LEFT JOIN user_permission AS userPerm ON user.id = userPerm.user_id

SELECT  order_date,order_status ,account_id ,table_id , item_id, item_price ,quantity 
FROM Orders, OrderDetails
WHERE Orders.order_id = OrderDetails.order_id
GROUP BY Orders.order_id;

select order_date, order_status, account_id, table_id, item_id , item_price , quantity from Orders  o inner join OrderDetails od  where o.order_id = od.order_id;

select * from Tables as t inner join Orders as o
on t.table_id = o.table_id
 where t.table_id =1  and t.table_status=1 and o.order_status = 0;
 
UPDATE Tables 
INNER JOIN Orders ON Tables.table_id = Orders.table_id
SET Tables.table_status = 0, Orders.order_status= 1
WHERE Orders.order_id = 1 ;
 
/*UPDATE OrderDetails 
INNER JOIN Orders
ON Orders.order_id = OrderDetails.order_id
SET OrderDetails.item_price=1, OrderDetails
WHERE bfbf ;

/*insert into Orders(table_id, order_status,account_id  ) values
	(1,0,1),(1,0,2),(1,0,3),
	(2,0,1),(2,0,2),(1,0,3),
	(3,0,1),(3,0,2),(3,0,3),
	(4,0,1),(4,0,2),(4,0,3),
	(5,0,1),(5,0,2),(5,0,3),
	(6,0,1),(6,0,2),(6,0,3),
	(7,0,1),(7,0,2),(7,0,3),
	(8,0,1),(8,0,2),(8,0,3),
	(9,0,1),(9,0,2),(9,0,3),
    (10,0,1),(10,0,2),(10,0,3),
	(11,0,1),(11,0,2),(11,0,3),
	(12,0,1),(12,0,2),(12,0,3),
    
	(13,0,4),(13,0,5),(13,0,6),
    (14,0,4),(14,0,5),(14,0,6),
    (15,0,4),(15,0,5),(15,0,6),
    (16,0,4),(16,0,5),(16,0,6),
    (17,0,4),(17,0,5),(17,0,6),
    (18,0,4),(18,0,5),(18,0,6),
    (19,0,4),(19,0,5),(19,0,6),
    (20,0,4),(20,0,5),(20,0,6),
    (21,0,4),(21,0,5),(21,0,6),
    (22,0,4),(22,0,5),(22,0,6),
    (23,0,4),(23,0,5),(23,0,6),
    (24,0,4),(24,0,5),(24,0,6),
    
    (25,0,7),(25,0,8),(25,0,9),
    (26,0,7),(26,0,8),(26,0,9),
    (27,0,7),(27,0,8),(27,0,9),
    (28,0,7),(28,0,8),(28,0,9),
    (29,0,7),(29,0,8),(29,0,9),
	(30,0,7),(30,0,8),(30,0,9),
    (31,0,7),(31,0,8),(31,0,9),
    (32,0,7),(32,0,8),(32,0,9),
    (33,0,7),(33,0,8),(33,0,9),
    (34,0,7),(34,0,8),(34,0,9),
    (35,0,7),(35,0,8),(35,0,9),
    (36,0,7),(36,0,8),(36,0,9); 
   
select * from Orders;
select * from OrderDetails;
select * from Items;
Update Orders set account_id = 2, table_id =1 where order_id = 1;
Update OrderDetails set item_id = 1, quantity=12 where order_id = 1;
DELETE  FROM Orders  
WHERE order_id = 1;*/

