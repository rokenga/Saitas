CREATE DATABASE saitas;

CREATE TABLE saitas.customer (
customer_id int,
customer_name varchar(255),
customer_surname varchar(255),
customer_birthDate date,
customer_address varchar(255),
customer_email varchar(255)
);

ALTER TABLE saitas.customer
ADD PRIMARY KEY (customer_id);

CREATE TABLE saitas.agentInfo (
agent_id int NOT NULL,
agent_name varchar(255),
agent_surname varchar(255),
PRIMARY KEY (agent_id)
);

CREATE TABLE saitas.contract (
contract_code int NOT NULL,
customer_id int,
agent_id int,
time_signed timestamp,
total_price decimal(10,2),
payment_time timestamp,
paid bool,
refunded bool,
refunded_time timestamp,
refunded_amount decimal(10,2),
PRIMARY KEY (contract_code)
);

INSERT INTO saitas.contract(contract_code, customer_id, agent_id, time_signed, total_price, payment_time, paid, refunded, refunded_time, refunded_amount)
VALUES('565', 1234, 333, "2023-01-24", '300.24', "2023-01-24", true, false, null, null);

SELECT * FROM saitas.contract;

ALTER TABLE saitas.contract
ADD FOREIGN KEY (customer_id) REFERENCES saitas.customer(customer_id);

ALTER TABLE saitas.contract
ADD FOREIGN KEY (agent_id) REFERENCES saitas.agentInfo(agent_id);

CREATE TABLE saitas.hotel(
contract_code int,
hotel_name varchar(255),
hotel_id int NOT NULL,
price decimal(10,2),
PRIMARY KEY (hotel_id),
FOREIGN KEY (contract_code) REFERENCES saitas.contract(contract_code)
);

INSERT INTO saitas.hotel(contract_code, hotel_name, hotel_id, price)
VALUES ('565', 'Akka','404040', '150.99');

CREATE TABLE saitas.transport(
contract_code int,
transport_id int NOT NULL,
price decimal(10,2),
PRIMARY KEY (transport_id),
FOREIGN KEY (contract_code) REFERENCES saitas.contract(contract_code)
);

INSERT INTO saitas.transport(contract_code, transport_id, price)
VALUES ('565', '888' ,'149.25');

INSERT INTO saitas.customer (customer_id, customer_name, customer_surname, customer_birthDate, customer_address, customer_email)
VALUES ('123', 'Augustinas', 'Labutis', '2000-05-30', 'Marijampolė, Vasario 16 g. 10-15', 'augustinas.labutis@gmail.com');

SELECT * FROM saitas.customer;

UPDATE saitas.customer
SET customer_id = '1234'
WHERE customer_surname = 'Labutis';

INSERT INTO saitas.agentInfo (agent_id, agent_name, agent_surname)
VALUES ('333', 'Lina', 'Rukaitytė');

SELECT * FROM saitas.agentInfo;

UPDATE saitas.agentInfo
SET agent_surname = 'Sveikatienė'
WHERE agent_id = '333';



