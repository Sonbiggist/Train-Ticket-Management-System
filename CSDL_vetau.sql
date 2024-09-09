create database Quanlivetau
use Quanlivetau
go

create table QUYEN
(
 MAQUYEN int primary key,
 TENQUYEN nvarchar(50)
)

--Bang NV 
create table NHANVIEN
(
 MANV nvarchar(50) primary key,
 TENNV nvarchar(50),
 NGAYSING datetime,
 GIOITINH nvarchar(50),
 DIACHI nvarchar(50),
 DTHOAI nvarchar(50),
 LUONG float,
 TENDN nvarchar(100),
 MK nvarchar(100),
 MAQUYEN int references QUYEN(MAQUYEN)
)

--Bang KH 
create table KHACHHANG 
(
 CMND nvarchar(50) primary key,
 TENKH nvarchar(50),
 NGAYSINH date,
 GIOITINH nvarchar(50),
 DIACHI nvarchar(50),
 DTHOAI nvarchar(50),
 EMAIL nvarchar(50),
 GHICHU nvarchar(50),
)

--Bang TAU
create table TAU
( 
 SOHIEU nvarchar(50) primary key,
 SOTOA int,
 SOGHE int 
)

--Bang ga tau
create table GATAU
(
 MAGA nvarchar(50) primary key,
 TENGA nvarchar(50) 
)

--Bang chuyen tau
create table CHUYENTAU
(
 MACT nvarchar(50) primary key,
 GAKHOIHANH nvarchar(50) references GATAU(MAGA),
 GADEN nvarchar(50) references GATAU(MAGA),
 GIODI datetime,
 GIODEN datetime,
 SOGHECON int,
 TRANGTHAI nvarchar(50),
 MATAU nvarchar(50) references TAU(SOHIEU)
)

--Bang dat cho
create table DATCHO
(
 MADCHO nvarchar(50) primary key,
 CMND nvarchar(50) references KHACHHANG(CMND),
 MACT nvarchar(50) references CHUYENTAU(MACT),
 SOLUONGVEMUA int,
 DONGIA float,
 THANHTIEN float,
 NGAYDATVE datetime,
 GHICHU nvarchar(100)
)

--Bang thanh toan
create table THANHTOAN
(
 MATT nvarchar(50) primary key,
 CMND nvarchar(50) references KHACHHANG(CMND),
 MADCHO nvarchar(50) references DATCHO(MADCHO),
 SOLUONGVEMUA int,
 THANHTIEN float,
 NGAYTHANHTOAN datetime,
 PHUONGTHUC nvarchar(50)
)

--Bang ve tau
create table VETAU
(
 MAVE nvarchar(50) primary key,
 CMND nvarchar(50) references KHACHHANG(CMND),
 MANV nvarchar(50) references NHANVIEN(MANV),
 MACT nvarchar(50) references CHUYENTAU(MACT),
 LOAIGHE nvarchar(50),
 GIAVE float,
 NGAYDATVE datetime,
 GHICHU nvarchar(100)
)

 select * from KHACHHANG
 select * from NHANVIEN
 select * from QUYEN
 select * from TAU
 select * from GATAU
 select * from CHUYENTAU
 select * from DATCHO
 select * from THANHTOAN
 select * from VETAU

-- Drop all tables
drop table THANHTOAN
drop table DATCHO
drop table CHUYENTAU
drop table GATAU
drop table TAU
drop table KHACHHANG
drop table NHANVIEN
drop table QUYEN



-- Inserting data into QUYEN table
insert into QUYEN values (1, N'Administrator')
insert into QUYEN values (2, N'Employee')

-- Inserting data into NHANVIEN table
insert into NHANVIEN values ('NV01', N'Nguyen Van Thanh', '2000-01-22', N'Male', N'112 Giai Phong', '0912648291', 0, 'admin', 'admin', 1)
insert into NHANVIEN values ('NV02', N'Nguyen Thi Tham', '2000-12-22', N'Fale', N'115 Giai Phong', '0913648291', 0, 'employee1', 'employee1', 2)
insert into NHANVIEN values ('NV03', N'Nguyen Truong Thanh', '1999-01-01', N'Male', N'104 Yen Lang', '0912648257', 0, 'employee2', 'employee2', 2)
insert into NHANVIEN values ('NV04', N'Tran Thanh Thuy', '1999-04-27', N'Fale', N'120 Yen Lang', '0912798257', 0, 'employee3', 'employee4', 2)
insert into NHANVIEN values ('NV05', N'Tran Dinh Tung', '1999-10-27', N'Male', N'120 Dong Da', '0912798987', 0, 'employee4', 'employee4', 2)

-- Inserting data into KHACHHANG table
insert into KHACHHANG values ('001303011257', N'Nguyen Thi Van Anh', '2003-01-01', N'Fmale', N'100 Giai Phong', '0912648291', 'vananh@gmail.com', '')
insert into KHACHHANG values ('001303012857', N'Nguyen Thi Hue', '2003-01-02', N'Fmale', N'100 Xa Dan', '0912948291', 'huenguyen@gmail.com', '')
insert into KHACHHANG values ('001303011357', N'Nguyen Tuan Phuong', '2003-12-01', N'Male', N'100 Hoang Cau', '0999648291', 'tuanphuong@gmail.com', '')
insert into KHACHHANG values ('001303451257', N'Hoang Van Son', '2003-07-13', N'Male', N'100 O Cho Dua', '0912848291', 'son123@gmail.com', '')
insert into KHACHHANG values ('001303011297', N'Le Ha Vy', '2001-11-01', N'Fmale', N'100 Pho Vong', '0914548291', 'havy12@gmail.com', '')

-- Inserting data into TAU table
insert into TAU values ('T01', 5, 250)
insert into TAU values ('T02', 4, 200)
insert into TAU values ('T03', 6, 300)
insert into TAU values ('T04', 7, 250)
insert into TAU values ('T05', 6, 250)

-- Inserting data into GATAU table
insert into GATAU values ('G01', 'Hanoi')
insert into GATAU values ('G02', 'Vinh')
insert into GATAU values ('G03', 'Ho Chi Minh City')
insert into GATAU values ('G04', 'Da Nang')
insert into GATAU values ('G05', 'Hai Phong')

-- Inserting data into CHUYENTAU table
insert into CHUYENTAU values ('CT01', 'G01', 'G02', '2024-03-13 13:00:00', '', 250, '', 'T01')
insert into CHUYENTAU values ('CT02', 'G01', 'G03', '2024-03-12 01:00:00', '', 250, '', 'T01')
insert into CHUYENTAU values ('CT03', 'G01', 'G04', '2024-03-13 13:30:00', '', 200, '', 'T02')
insert into CHUYENTAU values ('CT04', 'G01', 'G05', '2024-03-14 13:00:00', '', 250, '', 'T01')
insert into CHUYENTAU values ('CT05', 'G01', 'G02', '2024-03-15 13:00:00', '', 250, '', 'T01')

-- Inserting data into DATCHO table
INSERT INTO DATCHO (MADCHO, CMND, MACT, SOLUONGVEMUA, DONGIA, THANHTIEN, NGAYDATVE, GHICHU)
VALUES ('DC01', '001303011357', 'CT01', 2, 150000, 300000, '2024-03-15 13:00:00', 'Regular tickets'),
       ('DC02', '001303011357', 'CT02', 1, 200000, 200000, '2024-03-16 14:30:00', 'VIP tickets');

-- Inserting data into THANHTOAN table
INSERT INTO THANHTOAN (MATT, CMND, MADCHO, SOLUONGVEMUA, THANHTIEN, NGAYTHANHTOAN, PHUONGTHUC)
VALUES ('TT01', '001303011357', 'DC01', 2, 300000, '2024-03-15 13:00:00', 'Credit card'),
       ('TT02', '001303011357', 'DC02', 1, 200000, '2024-03-16 14:30:00', 'Cash');

-- Inserting data into VETAU table
INSERT INTO VETAU (MAVE, CMND, MANV, MACT, LOAIGHE, GIAVE, NGAYDATVE, GHICHU)
VALUES ('VT01', '001303011357', 'NV01', 'CT01', 'First class', 150000, '2024-03-15 13:00:00', 'Window seat'),
       ('VT02', '001303011357', 'NV02', 'CT02', 'Business class', 200000, '2024-03-16 14:30:00', 'Aisle seat');


ALTER PROC THONGKE @OPTION INT,@YEAR INT, @MONTH INT, @DAY INT
AS
BEGIN
--BAO CAO TONG
IF @OPTION = 1
	BEGIN
	SELECT YEAR(NGAYTHANHTOAN) AS NAM, SUM(SOLUONGVEMUA) AS TONG_SO_LUONG_VE_BAN, SUM(THANHTIEN) AS DOANH_SO
	FROM THANHTOAN
	GROUP BY YEAR(NGAYTHANHTOAN)
	END
ELSE IF @OPTION = 2
--BAO CAO THEO CAC NAM
	BEGIN
	SELECT MONTH(NGAYTHANHTOAN) AS THANG, SUM(SOLUONGVEMUA) AS TONG_SO_LUONG_VE_BAN, SUM(THANHTIEN) AS DOANH_SO
	FROM THANHTOAN
	WHERE YEAR(NGAYTHANHTOAN) = @YEAR
	GROUP BY MONTH(NGAYTHANHTOAN)
	END
ELSE IF @OPTION = 3
--BAO CAO THEO CAC THANG
	BEGIN
	SELECT CONVERT(DATE, NGAYTHANHTOAN) AS NGAY, SUM(SOLUONGVEMUA) AS TONG_SO_LUONG_VE_BAN, SUM(THANHTIEN) AS DOANH_SO
	FROM THANHTOAN
	WHERE YEAR(NGAYTHANHTOAN) = @YEAR AND MONTH(NGAYTHANHTOAN) = @MONTH
	GROUP BY NGAYTHANHTOAN
	END
END
SELECT OBJECT_ID('THONGKE') AS ObjectExists;

-- Total report (TONG)
EXEC THONGKE @OPTION = 1, @YEAR = NULL, @MONTH = NULL, @DAY = NULL;

-- Report by year (NAM)
EXEC THONGKE @OPTION = 2, @YEAR = 2024, @MONTH = NULL, @DAY = NULL;

-- Report by month (THANG)
EXEC THONGKE @OPTION = 3, @YEAR = 2023, @MONTH = 3, @DAY = NULL;

--Bao cao tong
CREATE VIEW V_THONGKE AS
SELECT YEAR(NGAYTHANHTOAN) AS NAM, SUM(SOLUONGVEMUA) AS TONG_SO_LUONG_VE_BAN, SUM(THANHTIEN) AS DOANH_SO
FROM THANHTOAN
GROUP BY YEAR(NGAYTHANHTOAN);

--BAOCAONGAY
alter proc sp_DSNGAY @YEAR INT, @MONTH INT, @DAY INT
as
begin
	select KHACHHANG.CMND, KHACHHANG.TENKH, SUM(SOLUONGVEMUA) as SO_LUONG_VE, CONVERT(TIME, NGAYTHANHTOAN) as GIO_THANH_TOAN, SUM(THANHTIEN) as DOANH_SO
	from THANHTOAN, KHACHHANG
	where KHACHHANG.CMND = THANHTOAN.CMND
	and YEAR(NGAYTHANHTOAN) = @YEAR
	and MONTH(NGAYTHANHTOAN) = @MONTH
	and DAY(NGAYTHANHTOAN) = @DAY
	group by KHACHHANG.CMND, KHACHHANG.TENKH, NGAYTHANHTOAN
end

exec sp_DSNGAY 2023, 3, 16


SELECT * FROM V_THONGKE

SELECT DISTINCT YEAR(NGAYTHANHTOAN) AS Nam FROM THANHTOAN