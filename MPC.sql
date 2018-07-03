drop database if exists MPC;

create database MPC;

use MPC;
	
create table Account(
	account_id int auto_increment primary key,
	username nvarchar(100) not null,
    password nvarchar(100) not null,
    staffname nvarchar(100) not null
    
);

create table Items_Category
( category_id int  auto_increment primary key,
food nvarchar(100) not null,
drink nvarchar(100) not null
);
create table Items(
	item_id int auto_increment primary key, 
    item_name nvarchar(200) not null,
    unit_price decimal(20,2) default 0   , 
    amount int not null default 0,
    status tinyint not null , /*0:hết sp, 1:chưa hết sp */
     category_id int not null,
     constraint fk_Items_Category foreign key( category_id) references Items_Category(category_id)
);
create table Tables
( table_id int auto_increment primary key,
table_name nvarchar(100),
	table_status tinyint not null default 0 /*0: trống, 1: đã có người*/
);
create table Orders(
	order_id int auto_increment primary key,
    order_date datetime default now(),
    order_status tinyint not null default 0, /*0: chưa thanh toán, 1: đã thanh toán, 2: hủy order*/
    account_id int not null,
    table_id int not null,
    constraint fk_Orders_Account foreign key(account_id) references Account(account_id),
    constraint fk_Orders_Tables foreign key(table_id) references Tables(table_id)
);

create table OrderDetails(
	order_id int not null,
    item_id int not null,
    unit_price decimal(20,2) not null ,
    quantity int not null default 1,
    constraint pk_OrderDetails primary key(order_id, item_id),
    constraint fk_OrderDetails_Orders foreign key(order_id) references Orders(order_id),
    constraint fk_OrderDetails_Items foreign key(item_id) references Items(item_id)
);

delimiter $$
create trigger tg_before_insert before insert
	on Items for each row
    begin
		if new.amount < 0 then
            signal sqlstate '45001' set message_text = 'tg_before_insert: amount must > 0';
        end if;
    end $$
delimiter ;

delimiter $$
create trigger tg_CheckAmount
	before update on Items
	for each row
	begin
		if new.amount < 0 then
            signal sqlstate '45001' set message_text = 'tg_CheckAmount: amount must > 0';
        end if;
    end $$
delimiter ;

/*insert data */
insert into Account(username,password ,staffname ) values 
('staff1','123456','An'),
('staff2','123456','Phương'),
('staff3','123456','Quân'),
('staff4','123456','Linh'),
('staff5','123456','Quyên'),
('staff6','123456','Quỳnh'),
('staff7','123456','Ngọc'),
('staff8','123456','Nhi'),
('staff9','123456','Bảo');
select * from Account;

insert into Items_Category( food ,drink) values
('khoai tây chiên','sữa chua đanh đá'),
('hướng dương','hoa quả dầm'),
('bánh ngọt','cafe sữa đá/nóng'), 
('mỳ ý','mojito bạc hà'),
('hamburger ','mojito việt quất'),
('xúc xích ','mojito dâu '),
('', 'mojito chanh'),
('','mojito xoài'),
('','mojito kiwi'),
('', 'trà nhài'),
('','trà đào'),
('','trà ô long'),
('', 'capuchino'),
('','caramel'),
('','mocha'),
('', 'coca'),
('','pepsi'),
('', 'sting');
 
select * from Items_Category;

insert into Items(item_name, unit_price, amount, status,category_id) values
	('khoai tây chiên', 20.000, 100, 1,1),
    ('hướng dương', 15.000, 50, 1,2),
    ('bánh ngọt', 20.000, 40, 1,3),
    ('mỳ ý', 45.000, 50, 1,4),
    ('hamburger',35.000, 100, 1,5),
    ('xúc xích',8.000, 100, 1,6),
    ('sữa chua đanh đá', 25.000, 100, 1,1),
    ('hoa quả dầm', 20.000, 50, 1,2),
    ('cafe sữa đá/nóng', 35.000, 100, 1,3),
    ('mojito bạc hà', 35.000, 100, 1,4),
    ('mojito việt quất', 35.000, 100, 1,5),
    ('mojito dâu ', 35.000, 100, 1,6),
    ('mojito chanh', 35.000, 100, 1,7),
	('mojito xoài',35.000, 100, 1,8),
	('mojito kiwi',35.000, 100, 1,9),
	('trà nhài',7.000, 40, 1,10),
    ('trà đào',23.000, 100, 1,11),
    ('trà ô long', 10.000, 30, 1,12),
	('capuchino', 55.000, 80, 1,13),
	('caramel', 55.000, 70, 1,14),
	('mocha', 45.000, 75, 1,15), 
	('coca', 10.000, 50, 1,16),
    ('pepsi',10.000, 40, 1,17),
    ('sting',10.000, 55, 1,18);
select * from Items;

insert into Tables( table_name,table_status) values
('bàn 1',0),('bàn 2',0),('bàn 3',0),('bàn 4',0),('bàn 5',0),('bàn 6',0),('bàn 7',0),('bàn 8',0),('bàn 9',0),
('bàn 10',0)('bàn 11',0),('bàn 12',0),('bàn 13',0),('bàn 14',0),('bàn 15',0),('bàn 16',0),('bàn 17',0),
('bàn 18',0),('bàn 19',0),('bàn 20',0),('bàn 21',0),('bàn 22',0),('bàn 23',0),('bàn 24',0),('bàn 25',0),('bàn 26',0),
('bàn 27',0),('bàn 28',0),('bàn 29',0),('bàn 30',0),('bàn 31',0),('bàn 32',0),('bàn 33',0),('bàn 34',0),('bàn 35',0),('bàn 36',0);
select * from Tables;
insert into Orders(table_id, order_status,account_id  ) values
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

insert into OrderDetails(order_id,item_id ,quantity ,unit_price) values
	(1, 1, 5, 100.000),
    (1, 2, 2, 30.000),
    (1, 3, 2, 40.0000);
select * from OrderDetails;
create user if not exists 'huyhomie66'@'localhost' identified by '123456789';
grant all on MPC_SQL.* to 'huyhomie66'@'localhost';
-- grant all on Items to 'vtca'@'localhost';
-- grant all on Customers to 'vtca'@'localhost';
-- grant all on Orders to 'vtca'@'localhost';
-- grant all on OrderDetails to 'vtca'@'localhost';

/* INSERT DATA 
insert into Customers(customer_name, customer_address) values
	('Nguyen Thi X','Hai Duong'),
    ('Nguyen Van N','Hanoi'),
    ('Nguyen Van B','Ho Chi Minh'),
    ('Nguyen Van A','Hanoi');
select * from Customers;

insert into Items(item_name, unit_price, amount, item_status) values
	('Item 1', 12.5, 8, 1),
    ('Item 2', 62.5, 6, 1),
    ('Item 3', 31.0, 10, 1),
    ('Item 4', 24.5, 5, 1),
    ('Item 5', 7.5, 9, 1);
select * from Items;

insert into Orders(customer_id, order_status) values
	(1, 1), (2, 1), (1, 1);
select * from Orders;

insert into OrderDetails(order_id, item_id, unit_price, quantity) values
	(1, 1, 12.5, 5), (1, 3, 31.0, 1), (1, 4, 24.5, 2),
    (2, 2, 62.5, 1), (2, 3, 31.0, 2), (2, 5, 7.5, 4);
select * from OrderDetails;

/* CREATE & GRANT USER 
create user if not exists 'vtca'@'localhost' identified by 'vtcacademy';
grant all on OrderDB.* to 'vtca'@'localhost';
-- grant all on Items to 'vtca'@'localhost';
-- grant all on Customers to 'vtca'@'localhost';
-- grant all on Orders to 'vtca'@'localhost';
-- grant all on OrderDetails to 'vtca'@'localhost';

select item_id from Items order by item_id desc limit 1;

select customer_id, customer_name,
    ifnull(customer_address, '') as customer_address
from Customers where customer_id=1;
                        
select order_id from Orders order by order_id desc limit 1;

select LAST_INSERT_ID();
select customer_id from Customers order by customer_id desc limit 1;

update Items set amount=10 where item_id=3;
-- lock table Orders write;
-- unlock tables;