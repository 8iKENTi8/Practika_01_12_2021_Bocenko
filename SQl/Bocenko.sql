-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3307
-- Generation Time: Dec 02, 2021 at 08:18 PM
-- Server version: 5.7.24
-- PHP Version: 7.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `ekz`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_empl` (IN `dol` VARCHAR(40), IN `Fa` VARCHAR(40), IN `Name` VARCHAR(40), IN `Ot` VARCHAR(40), IN `dateR` DATE)  NO SQL
BEGIN
DECLARE ID_C int;
SET ID_C = (SELECT `должность`.`id_do` FROM `должность` WHERE `должность`.`Название`=dol);

INSERT INTO `сотрудник` 
(`id_c`, `Фамилия`, `Имя`,`Отчество`, `Дата_рождения`, `id_do`) 
VALUES (NULL, Fa, Name, Ot, dateR, ID_C);

END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `договор`
--

CREATE TABLE `договор` (
  `id_d` int(3) UNSIGNED NOT NULL,
  `id_k` int(3) UNSIGNED NOT NULL,
  `id_c` int(3) UNSIGNED NOT NULL,
  `id_y` int(3) UNSIGNED NOT NULL,
  `Дата_Закл` date NOT NULL,
  `Дата_Окон` date NOT NULL,
  `Итог_дней` int(5) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Triggers `договор`
--
DELIMITER $$
CREATE TRIGGER `Итог_с` BEFORE INSERT ON `договор` FOR EACH ROW SET NEW.Итог_дней = DAYOFMONTH(NEW.Дата_Окон)- DAYOFMONTH(NEW.Дата_Закл)
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `Итог_с_обнов` BEFORE UPDATE ON `договор` FOR EACH ROW SET NEW.Итог_дней = DAYOFMONTH(NEW.Дата_Окон)- DAYOFMONTH(NEW.Дата_Закл)
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `должность`
--

CREATE TABLE `должность` (
  `id_do` int(3) UNSIGNED NOT NULL,
  `Название` varchar(40) NOT NULL,
  `Зарплата` int(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `должность`
--

INSERT INTO `должность` (`id_do`, `Название`, `Зарплата`) VALUES
(1, 'Менеджер ', 67000),
(2, 'Директор отдела', 100001),
(3, 'Админ', 123456);

-- --------------------------------------------------------

--
-- Table structure for table `клиент`
--

CREATE TABLE `клиент` (
  `id_k` int(3) UNSIGNED NOT NULL,
  `Компания` varchar(40) NOT NULL,
  `Телефон` varchar(11) DEFAULT NULL,
  `Mail` varchar(40) NOT NULL,
  `Адрес` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `клиент`
--

INSERT INTO `клиент` (`id_k`, `Компания`, `Телефон`, `Mail`, `Адрес`) VALUES
(1, 'Asus1', '89150644978', 'asus1@gg.ru', 'г. Моска'),
(3, 'afasfas', '89150644971', 'asus@gmail.comasf', 'г. Моска'),
(5, 'Газпром', '89134521', 'fasfsa@mail.ru', 'г. Питер');

-- --------------------------------------------------------

--
-- Table structure for table `сотрудник`
--

CREATE TABLE `сотрудник` (
  `id_c` int(3) UNSIGNED NOT NULL,
  `Фамилия` varchar(40) NOT NULL,
  `Имя` varchar(30) NOT NULL,
  `Отчество` varchar(30) NOT NULL,
  `Дата_рождения` date NOT NULL,
  `id_do` int(3) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `сотрудник`
--

INSERT INTO `сотрудник` (`id_c`, `Фамилия`, `Имя`, `Отчество`, `Дата_рождения`, `id_do`) VALUES
(1, 'Ролдугин', 'Владимир', 'Дмитриевич', '2021-08-03', 1),
(4, 'afsfasfasafs', 'afssf', 'sffs', '2022-02-20', 1),
(5, 'fsa', 'asffas', 'asff', '2022-12-12', 3),
(7, 'фа', 'фыа', 'афы', '2002-12-11', 1),
(8, 'afsfas', 'sfaasf', 'saffas', '2020-02-20', 2),
(9, 'fsf2', 'saffs23', 'asffsa', '2001-02-22', 3);

-- --------------------------------------------------------

--
-- Table structure for table `услуга`
--

CREATE TABLE `услуга` (
  `id_y` int(3) UNSIGNED NOT NULL,
  `Название` varchar(100) NOT NULL,
  `Стоимость` int(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `услуга`
--

INSERT INTO `услуга` (`id_y`, `Название`, `Стоимость`) VALUES
(1, 'Поставить новое оборудование в сервисной', 54034),
(2, 'Заменить освещение в определенном месте ', 21342),
(3, 'Починить пк', 234),
(6, 'zaffasfas', 2131);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `договор`
--
ALTER TABLE `договор`
  ADD PRIMARY KEY (`id_d`),
  ADD KEY `работник` (`id_c`),
  ADD KEY `услуга` (`id_y`),
  ADD KEY `клиен` (`id_k`);

--
-- Indexes for table `должность`
--
ALTER TABLE `должность`
  ADD PRIMARY KEY (`id_do`);

--
-- Indexes for table `клиент`
--
ALTER TABLE `клиент`
  ADD PRIMARY KEY (`id_k`);

--
-- Indexes for table `сотрудник`
--
ALTER TABLE `сотрудник`
  ADD PRIMARY KEY (`id_c`),
  ADD KEY `id_do` (`id_do`);

--
-- Indexes for table `услуга`
--
ALTER TABLE `услуга`
  ADD PRIMARY KEY (`id_y`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `договор`
--
ALTER TABLE `договор`
  MODIFY `id_d` int(3) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `должность`
--
ALTER TABLE `должность`
  MODIFY `id_do` int(3) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `клиент`
--
ALTER TABLE `клиент`
  MODIFY `id_k` int(3) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `сотрудник`
--
ALTER TABLE `сотрудник`
  MODIFY `id_c` int(3) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `услуга`
--
ALTER TABLE `услуга`
  MODIFY `id_y` int(3) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `договор`
--
ALTER TABLE `договор`
  ADD CONSTRAINT `договор_ibfk_1` FOREIGN KEY (`id_y`) REFERENCES `услуга` (`id_y`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `клиен` FOREIGN KEY (`id_k`) REFERENCES `клиент` (`id_k`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `работник` FOREIGN KEY (`id_c`) REFERENCES `сотрудник` (`id_c`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `услуга` FOREIGN KEY (`id_y`) REFERENCES `услуга` (`id_y`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `сотрудник`
--
ALTER TABLE `сотрудник`
  ADD CONSTRAINT `сотрудник_ibfk_1` FOREIGN KEY (`id_do`) REFERENCES `должность` (`id_do`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
