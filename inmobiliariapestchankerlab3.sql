-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 30-10-2022 a las 23:16:01
-- Versión del servidor: 10.4.22-MariaDB
-- Versión de PHP: 8.1.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliariapestchankerlab3`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `Id` int(11) NOT NULL,
  `InmuebleId` int(11) NOT NULL,
  `InquilinoId` int(11) NOT NULL,
  `FechaInicio` date NOT NULL,
  `FechaFin` date NOT NULL,
  `Precio` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`Id`, `InmuebleId`, `InquilinoId`, `FechaInicio`, `FechaFin`, `Precio`) VALUES
(25, 47, 6, '2022-10-02', '2022-11-30', '70000'),
(26, 48, 7, '2022-10-01', '2022-10-19', '110000');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `Id` int(11) NOT NULL,
  `PropietarioId` int(11) NOT NULL,
  `Direccion` varchar(100) NOT NULL,
  `CoordenadaN` decimal(10,5) NOT NULL,
  `CoordenadaE` decimal(10,5) NOT NULL,
  `TipoInmuebleId` int(11) NOT NULL,
  `TipoUso` varchar(10) NOT NULL,
  `CantidadAmbientes` int(11) NOT NULL,
  `PrecioInmueble` decimal(10,2) NOT NULL,
  `Disponible` tinyint(1) NOT NULL,
  `Imagen` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`Id`, `PropietarioId`, `Direccion`, `CoordenadaN`, `CoordenadaE`, `TipoInmuebleId`, `TipoUso`, `CantidadAmbientes`, `PrecioInmueble`, `Disponible`, `Imagen`) VALUES
(47, 8, 'Pringles 333', '-66.33188', '-33.29452', 1, 'Part', 6, '80000.00', 1, 'http://192.168.100.4:5000/Uploads/inmueble_83649.jpg'),
(48, 8, 'Junin 444', '-68.00332', '-44.03952', 2, 'Clinica', 10, '125000.00', 1, 'http://192.168.100.4:5000/Uploads/inmueble_895997.jpg'),
(49, 8, 'San Martin 999', '-66.33186', '-33.29452', 1, 'Casa', 8, '80000.00', 1, 'http://192.168.100.4:5000/Uploads/inmueble_893015.jpg');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Dni` varchar(20) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `Email` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`Id`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`) VALUES
(6, 'Laura', 'Camargo', '6666666', '6666666', 'laura@gmail.com'),
(7, 'Sofia', 'Lucero', '8888888', '8888888', 'sofia@gmail.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `Id` int(11) NOT NULL,
  `FechaPago` date NOT NULL,
  `Importe` decimal(10,0) NOT NULL,
  `IdContrato` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Dni` varchar(20) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Clave` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`Id`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`, `Clave`) VALUES
(5, 'Juan Ignacio', 'Lopez', '44444444', '435222', 'juan@gmail.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA='),
(8, 'Federico', 'Pestchanker', '22222222', '33333333', 'federico@ncdsanluis.com.ar', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoinmueble`
--

CREATE TABLE `tipoinmueble` (
  `Id` int(11) NOT NULL,
  `Descripcion` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `tipoinmueble`
--

INSERT INTO `tipoinmueble` (`Id`, `Descripcion`) VALUES
(1, 'Casa'),
(2, 'Local');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdInmueble` (`InmuebleId`),
  ADD KEY `IdInquilino` (`InquilinoId`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Direccion` (`Direccion`),
  ADD KEY `IdPropietario` (`PropietarioId`),
  ADD KEY `IdTipoInmueble` (`TipoInmuebleId`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdContrato` (`IdContrato`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=50;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`InmuebleId`) REFERENCES `inmueble` (`Id`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`InquilinoId`) REFERENCES `inquilino` (`Id`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`TipoInmuebleId`) REFERENCES `tipoinmueble` (`Id`),
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`PropietarioId`) REFERENCES `propietario` (`Id`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`IdContrato`) REFERENCES `contrato` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
