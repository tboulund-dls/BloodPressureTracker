CREATE DATABASE bm_db;
USE bm_db;

CREATE TABLE patients (
    ssn CHAR(10) NOT NULL,
    mail VARCHAR(128) NOT NULL,
    name VARCHAR(50) NOT NULL,
    PRIMARY KEY (ssn)
);

CREATE TABLE measurements (
    id INT UNSIGNED AUTO_INCREMENT,
    ssn CHAR(10) NOT NULL,
    date CHAR(19) NOT NULL,
    systolic INT UNSIGNED NOT NULL,
    diastolic INT UNSIGNED NOT NULL,
    seen TINYINT UNSIGNED NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (ssn) REFERENCES patients(ssn) ON DELETE CASCADE
);

CREATE INDEX idx_measurement_date ON measurements(date);
CREATE INDEX idx_measurement_ssn ON measurements(ssn);

INSERT INTO patients (ssn, mail, name) VALUES 
(   '1234567590', 
    'Mette@outlook.com', 
    'Mette Lykke Slot'
);

INSERT INTO patients (ssn, mail, name) VALUES 
(   '1234567891', 
    'Benny@gmail.com', 
    'Benny Hansen'
);

INSERT INTO patients (ssn, mail, name) VALUES 
(   '1234567893', 
    'Jens@hotmail.com', 
    'Jens løg Hansen'
);

INSERT INTO patients (ssn, mail, name) VALUES 
(   '1234567453', 
    'Janni@hotmail.com', 
    'Janni Sloth Nielsen'
);

INSERT INTO patients (ssn, mail, name) VALUES 
(   '1236567453', 
    'Janni@hotmail.com', 
    'Tina Sloth Nielsen'
);

INSERT INTO measurements (ssn, date, systolic, diastolic, seen) 
VALUES 
('1234567891', '2023-11-07 12:30:00', 120, 80, 1),
('1234567893', '2023-11-08 14:15:00', 144, 75, 0),
('1236567453', '2023-11-08 14:15:00', 130, 65, 0),
('1234567893', '2023-11-08 14:15:00', 155, 55, 0),
('1234567893', '2023-11-08 14:15:00', 137, 45, 0),
('1234567453', '2023-11-08 14:15:00', 233, 35, 0),
('1234567590', '2023-11-08 14:15:00', 139, 35, 0);
