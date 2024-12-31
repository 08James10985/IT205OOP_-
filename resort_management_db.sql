-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 31, 2024 at 07:47 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `resort_management_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `combomeals`
--

CREATE TABLE `combomeals` (
  `Id` int(11) NOT NULL,
  `MealName` varchar(100) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `combomeals`
--

INSERT INTO `combomeals` (`Id`, `MealName`, `Price`, `Description`) VALUES
(1, 'Tropical Delight', 150.00, 'A refreshing combo of grilled fish, rice, and tropical salad.'),
(2, 'Mountain Feast', 200.00, 'A hearty meal with roasted chicken, mashed potatoes, and steamed veggies.'),
(3, 'Ocean Breeze', 175.00, 'Delicious shrimp pasta with garlic bread and a side salad.'),
(4, 'Island Special', 220.00, 'Grilled steak with mango salsa, served with fries and coleslaw.'),
(5, 'Veggie Delight', 130.00, 'A flavorful mix of grilled vegetables, quinoa, and hummus.'),
(8, 'lansang', 23.00, 'crunchy');

-- --------------------------------------------------------

--
-- Table structure for table `conferencerooms`
--

CREATE TABLE `conferencerooms` (
  `Id` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Capacity` int(11) NOT NULL,
  `IsAvailable` tinyint(1) NOT NULL,
  `CateringServices` tinyint(1) NOT NULL,
  `HasTechnicalSupport` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `conferencerooms`
--

INSERT INTO `conferencerooms` (`Id`, `Name`, `Capacity`, `IsAvailable`, `CateringServices`, `HasTechnicalSupport`) VALUES
(1, 'Ocean View Hall', 50, 1, 1, 1),
(2, 'Coral Reef Room', 30, 1, 0, 1),
(3, 'Sunset Lounge', 40, 0, 1, 0),
(4, 'Beachfront Pavilion', 100, 1, 1, 1),
(5, 'Seaside Conference Center', 70, 0, 0, 1),
(6, 'Lagoon View Hall', 60, 1, 1, 0),
(7, 'Palm Grove Hall', 20, 1, 0, 0),
(8, 'Tropical Oasis Room', 80, 0, 1, 1),
(9, 'Garden View Pavilion', 90, 1, 1, 0),
(10, 'Coconut Grove Center', 120, 1, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `cottagereefs`
--

CREATE TABLE `cottagereefs` (
  `Id` int(11) NOT NULL,
  `ActivityName` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `IsAvailable` tinyint(1) NOT NULL,
  `EquipmentRentalPrice` decimal(10,2) NOT NULL,
  `Schedule` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `cottagereefs`
--

INSERT INTO `cottagereefs` (`Id`, `ActivityName`, `Description`, `IsAvailable`, `EquipmentRentalPrice`, `Schedule`) VALUES
(1, 'Snorkeling Adventure', 'Explore the vibrant coral reefs and marine life.', 1, 20.00, '9 AM - 12 PM'),
(2, 'Kayaking', 'Paddle through calm waters and enjoy the scenery.', 0, 15.00, '1 PM - 4 PM'),
(3, 'Beach Volleyball', 'Join a game on the beach with friends and family.', 1, 5.00, '3 PM - 6 PM'),
(4, 'Stand Up Paddleboarding', 'Experience the beauty of the water while paddling.', 1, 25.00, '8 AM - 11 AM'),
(5, 'Fishing Trip', 'Catch fresh fish with our guided fishing tours.', 0, 30.00, '5 AM - 10 AM'),
(6, 'Jet Skiing', 'Get your adrenaline pumping with exciting jet ski rides.', 1, 50.00, '10 AM - 1 PM'),
(7, 'Sunset Cruise', 'Enjoy a relaxing cruise during sunset.', 1, 40.00, '5 PM - 7 PM'),
(8, 'Beach Bonfire', 'Gather around a bonfire for a night of fun.', 1, 10.00, '7 PM - 10 PM'),
(9, 'Guided Nature Walk', 'Explore the local flora and fauna with a guide.', 1, 15.00, '8 AM - 10 AM'),
(10, 'Scuba Diving', 'Dive into the depths and discover underwater treasures.', 0, 60.00, '11 AM - 3 PM');

-- --------------------------------------------------------

--
-- Table structure for table `cottages`
--

CREATE TABLE `cottages` (
  `Id` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `IsAvailable` tinyint(1) NOT NULL,
  `Capacity` int(11) NOT NULL,
  `CheckIn` datetime NOT NULL,
  `CheckOut` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `cottages`
--

INSERT INTO `cottages` (`Id`, `Name`, `IsAvailable`, `Capacity`, `CheckIn`, `CheckOut`) VALUES
(1, 'Beachside Bungalow', 1, 4, '2024-12-01 00:00:00', '2024-12-10 00:00:00'),
(2, 'Mountain Retreat', 0, 6, '2024-12-01 00:00:00', '2024-12-10 00:00:00'),
(3, 'Lakeside Cabin', 1, 2, '2024-12-01 00:00:00', '2024-12-10 00:00:00'),
(4, 'Forest Lodge', 1, 5, '2024-12-01 00:00:00', '2024-12-10 00:00:00'),
(5, 'Garden Cottage', 0, 3, '2024-12-01 00:00:00', '2024-12-10 00:00:00'),
(6, 'Cozy Hideaway', 1, 4, '2024-12-01 00:00:00', '2024-12-10 00:00:00'),
(7, 'Ocean View Retreat', 1, 8, '2024-12-01 00:00:00', '2024-12-10 00:00:00'),
(8, 'Sunny Villa', 0, 10, '2024-12-01 00:00:00', '2024-12-10 00:00:00'),
(9, 'Charming Cottage', 1, 2, '2024-12-01 00:00:00', '2024-12-10 00:00:00'),
(10, 'Secluded Escape', 1, 6, '2024-12-01 00:00:00', '2024-12-10 00:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `packages`
--

CREATE TABLE `packages` (
  `Id` int(11) NOT NULL,
  `PackageName` varchar(100) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `packages`
--

INSERT INTO `packages` (`Id`, `PackageName`, `Price`, `Description`) VALUES
(1, 'Honeymoon Package', 999.99, 'Romantic 3-day getaway including spa treatments and candlelit dinners'),
(2, 'Family Fun Package', 799.99, 'Perfect for families - includes water activities and kids club access'),
(3, 'Adventure Package', 599.99, 'Includes hiking, kayaking, and snorkeling adventures'),
(4, 'Wellness Retreat', 899.99, 'Focus on relaxation with yoga classes and spa treatments'),
(5, 'Weekend Getaway', 499.99, '2-day escape with breakfast and dinner included');

-- --------------------------------------------------------

--
-- Table structure for table `trinaryhalls`
--

CREATE TABLE `trinaryhalls` (
  `Id` int(11) NOT NULL,
  `HallName` varchar(100) NOT NULL,
  `Capacity` int(11) NOT NULL,
  `IsAvailable` tinyint(1) NOT NULL,
  `RentalPricePerHour` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `trinaryhalls`
--

INSERT INTO `trinaryhalls` (`Id`, `HallName`, `Capacity`, `IsAvailable`, `RentalPricePerHour`) VALUES
(1, 'Main Hall', 200, 1, 500.00),
(2, 'Garden Hall', 150, 0, 400.00),
(3, 'Conference Hall', 100, 1, 300.00),
(4, 'Banquet Hall', 250, 1, 600.00),
(5, 'Exhibition Hall', 300, 0, 700.00),
(6, 'Workshop Hall', 80, 1, 250.00),
(7, 'Ballroom', 350, 1, 800.00),
(8, 'VIP Hall', 50, 1, 900.00),
(9, 'Outdoor Tent', 120, 0, 350.00),
(10, 'Small Conference Room', 30, 1, 200.00);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserId` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `PasswordHash` varchar(256) NOT NULL,
  `PasswordSalt` varchar(256) NOT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Platform` varchar(50) DEFAULT NULL,
  `Role` varchar(50) DEFAULT 'user',
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserId`, `Username`, `Email`, `PasswordHash`, `PasswordSalt`, `FirstName`, `LastName`, `Platform`, `Role`, `CreatedAt`) VALUES
(3, 'admin', 'jamesesma243@gmail.com', '3db5f4356ca5eca9852caa173d1294b46593ff50f0ba4a7f97774da0828fd3ff', '3db5f4356ca5eca9852caa173d1294b46593ff50f0ba4a7f97774da0828fd3ff', 'james', 'esma', 'web', 'admin', '2024-12-06 16:16:47'),
(4, 'elmir', 'elpph', 'a498f5d7b20fe4455aaa41f98c2a469273c77f3cabba1259db77ad1e3d480e1f', 'a498f5d7b20fe4455aaa41f98c2a469273c77f3cabba1259db77ad1e3d480e1f', 'elmirg', 'gonz', 'web', 'user', '2024-12-07 12:18:15');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `combomeals`
--
ALTER TABLE `combomeals`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `conferencerooms`
--
ALTER TABLE `conferencerooms`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `cottagereefs`
--
ALTER TABLE `cottagereefs`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `cottages`
--
ALTER TABLE `cottages`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `packages`
--
ALTER TABLE `packages`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `trinaryhalls`
--
ALTER TABLE `trinaryhalls`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserId`),
  ADD UNIQUE KEY `Username` (`Username`),
  ADD UNIQUE KEY `Email` (`Email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `combomeals`
--
ALTER TABLE `combomeals`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `conferencerooms`
--
ALTER TABLE `conferencerooms`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `cottagereefs`
--
ALTER TABLE `cottagereefs`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `cottages`
--
ALTER TABLE `cottages`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `packages`
--
ALTER TABLE `packages`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `trinaryhalls`
--
ALTER TABLE `trinaryhalls`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `UserId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
