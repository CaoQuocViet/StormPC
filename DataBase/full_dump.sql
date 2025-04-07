--
-- PostgreSQL database dump
--

-- Dumped from database version 16.8 (Debian 16.8-1.pgdg120+1)
-- Dumped by pg_dump version 16.6 (Ubuntu 16.6-0ubuntu0.24.04.1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Brands; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."Brands" (
    "BrandID" integer NOT NULL,
    "BrandName" character varying(255),
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."Brands" OWNER TO vietcq;

--
-- Name: Brands_BrandID_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Brands_BrandID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Brands_BrandID_seq" OWNER TO vietcq;

--
-- Name: Brands_BrandID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Brands_BrandID_seq" OWNED BY public."Brands"."BrandID";


--
-- Name: Categories; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."Categories" (
    "CategoryID" integer NOT NULL,
    "CategoryName" character varying(255),
    "Description" text,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."Categories" OWNER TO vietcq;

--
-- Name: Categories_CategoryID_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Categories_CategoryID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Categories_CategoryID_seq" OWNER TO vietcq;

--
-- Name: Categories_CategoryID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Categories_CategoryID_seq" OWNED BY public."Categories"."CategoryID";


--
-- Name: Cities; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."Cities" (
    id integer NOT NULL,
    "CityCode" character varying(255),
    "CityName" character varying(255),
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."Cities" OWNER TO vietcq;

--
-- Name: Cities_id_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Cities_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Cities_id_seq" OWNER TO vietcq;

--
-- Name: Cities_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Cities_id_seq" OWNED BY public."Cities".id;


--
-- Name: Customers; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."Customers" (
    "CustomerID" integer NOT NULL,
    "FullName" character varying(255),
    "Email" character varying(255),
    "Phone" character varying(255),
    "Address" text,
    "CityCode" integer NOT NULL,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."Customers" OWNER TO vietcq;

--
-- Name: Customers_CustomerID_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Customers_CustomerID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Customers_CustomerID_seq" OWNER TO vietcq;

--
-- Name: Customers_CustomerID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Customers_CustomerID_seq" OWNED BY public."Customers"."CustomerID";


--
-- Name: LaptopSpecs; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."LaptopSpecs" (
    "VariantID" integer NOT NULL,
    "SKU" character varying(255) NOT NULL,
    "LaptopID" integer,
    "CPU" character varying(255),
    "GPU" character varying(255),
    "RAM" integer,
    "Storage" integer,
    "StorageType" character varying(255),
    "Color" character varying(255),
    "ImportPrice" numeric NOT NULL,
    "Price" numeric,
    "StockQuantity" integer,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."LaptopSpecs" OWNER TO vietcq;

--
-- Name: LaptopSpecs_VariantID_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."LaptopSpecs_VariantID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."LaptopSpecs_VariantID_seq" OWNER TO vietcq;

--
-- Name: LaptopSpecs_VariantID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."LaptopSpecs_VariantID_seq" OWNED BY public."LaptopSpecs"."VariantID";


--
-- Name: Laptops; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."Laptops" (
    "LaptopID" integer NOT NULL,
    "BrandID" integer,
    "CategoryID" integer,
    "Picture" character varying(255),
    "ModelName" character varying(255),
    "ScreenSize" numeric,
    "OperatingSystem" character varying(255),
    "ReleaseYear" integer,
    "Description" text,
    "Discount" numeric NOT NULL,
    "DiscountEndDate" timestamp with time zone,
    "DiscountStartDate" timestamp with time zone,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."Laptops" OWNER TO vietcq;

--
-- Name: Laptops_LaptopID_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Laptops_LaptopID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Laptops_LaptopID_seq" OWNER TO vietcq;

--
-- Name: Laptops_LaptopID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Laptops_LaptopID_seq" OWNED BY public."Laptops"."LaptopID";


--
-- Name: OrderItems; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."OrderItems" (
    id integer NOT NULL,
    "OrderID" integer,
    "VariantID" integer,
    "Quantity" integer,
    "UnitPrice" numeric,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."OrderItems" OWNER TO vietcq;

--
-- Name: OrderItems_id_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."OrderItems_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."OrderItems_id_seq" OWNER TO vietcq;

--
-- Name: OrderItems_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."OrderItems_id_seq" OWNED BY public."OrderItems".id;


--
-- Name: OrderStatuses; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."OrderStatuses" (
    "StatusID" integer NOT NULL,
    "StatusName" character varying(255),
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."OrderStatuses" OWNER TO vietcq;

--
-- Name: OrderStatuses_StatusID_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."OrderStatuses_StatusID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."OrderStatuses_StatusID_seq" OWNER TO vietcq;

--
-- Name: OrderStatuses_StatusID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."OrderStatuses_StatusID_seq" OWNED BY public."OrderStatuses"."StatusID";


--
-- Name: Orders; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."Orders" (
    "OrderID" integer NOT NULL,
    "CustomerID" integer,
    "OrderDate" timestamp with time zone,
    "StatusID" integer,
    "TotalAmount" numeric,
    "PaymentMethodID" integer,
    "ShipCityID" integer,
    "ShippingAddress" text,
    "ShippingCity" character varying(255),
    "ShippingPostalCode" character varying(10),
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."Orders" OWNER TO vietcq;

--
-- Name: Orders_OrderID_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Orders_OrderID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Orders_OrderID_seq" OWNER TO vietcq;

--
-- Name: Orders_OrderID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Orders_OrderID_seq" OWNED BY public."Orders"."OrderID";


--
-- Name: PaymentMethods; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."PaymentMethods" (
    "PaymentMethodID" integer NOT NULL,
    "MethodName" character varying(255),
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."PaymentMethods" OWNER TO vietcq;

--
-- Name: PaymentMethods_PaymentMethodID_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."PaymentMethods_PaymentMethodID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."PaymentMethods_PaymentMethodID_seq" OWNER TO vietcq;

--
-- Name: PaymentMethods_PaymentMethodID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."PaymentMethods_PaymentMethodID_seq" OWNED BY public."PaymentMethods"."PaymentMethodID";


--
-- Name: SequelizeMeta; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."SequelizeMeta" (
    name character varying(255) NOT NULL
);


ALTER TABLE public."SequelizeMeta" OWNER TO vietcq;

--
-- Name: SoftwareVersion; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."SoftwareVersion" (
    "Version" character varying(255),
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."SoftwareVersion" OWNER TO vietcq;

--
-- Name: Brands BrandID; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Brands" ALTER COLUMN "BrandID" SET DEFAULT nextval('public."Brands_BrandID_seq"'::regclass);


--
-- Name: Categories CategoryID; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Categories" ALTER COLUMN "CategoryID" SET DEFAULT nextval('public."Categories_CategoryID_seq"'::regclass);


--
-- Name: Cities id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Cities" ALTER COLUMN id SET DEFAULT nextval('public."Cities_id_seq"'::regclass);


--
-- Name: Customers CustomerID; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Customers" ALTER COLUMN "CustomerID" SET DEFAULT nextval('public."Customers_CustomerID_seq"'::regclass);


--
-- Name: LaptopSpecs VariantID; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."LaptopSpecs" ALTER COLUMN "VariantID" SET DEFAULT nextval('public."LaptopSpecs_VariantID_seq"'::regclass);


--
-- Name: Laptops LaptopID; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Laptops" ALTER COLUMN "LaptopID" SET DEFAULT nextval('public."Laptops_LaptopID_seq"'::regclass);


--
-- Name: OrderItems id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."OrderItems" ALTER COLUMN id SET DEFAULT nextval('public."OrderItems_id_seq"'::regclass);


--
-- Name: OrderStatuses StatusID; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."OrderStatuses" ALTER COLUMN "StatusID" SET DEFAULT nextval('public."OrderStatuses_StatusID_seq"'::regclass);


--
-- Name: Orders OrderID; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Orders" ALTER COLUMN "OrderID" SET DEFAULT nextval('public."Orders_OrderID_seq"'::regclass);


--
-- Name: PaymentMethods PaymentMethodID; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."PaymentMethods" ALTER COLUMN "PaymentMethodID" SET DEFAULT nextval('public."PaymentMethods_PaymentMethodID_seq"'::regclass);


--
-- Data for Name: Brands; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."Brands" ("BrandID", "BrandName", "IsDeleted", "createdAt", "updatedAt") FROM stdin;
1	Dell	f	2025-01-05 09:30:00+00	2025-01-05 09:30:00+00
2	Asus	f	2025-01-05 10:15:00+00	2025-01-05 10:15:00+00
3	Lenovo	f	2025-01-05 11:20:00+00	2025-01-05 11:20:00+00
4	HP	f	2025-01-05 13:45:00+00	2025-01-05 13:45:00+00
5	Razer	f	2025-01-06 09:10:00+00	2025-01-06 09:10:00+00
6	LG	f	2025-01-06 10:30:00+00	2025-01-06 10:30:00+00
7	Acer	f	2025-01-06 14:25:00+00	2025-01-06 14:25:00+00
8	MSI	f	2025-01-07 08:50:00+00	2025-01-07 08:50:00+00
9	Gigabyte	f	2025-01-07 11:15:00+00	2025-01-07 11:15:00+00
10	Apple	f	2025-01-07 15:40:00+00	2025-01-07 15:40:00+00
\.


--
-- Data for Name: Categories; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."Categories" ("CategoryID", "CategoryName", "Description", "IsDeleted", "createdAt", "updatedAt") FROM stdin;
1	Văn phòng	Dành cho công việc văn phòng, pin lâu, nhẹ	f	2025-01-10 08:30:00+00	2025-01-10 08:30:00+00
2	Gaming	Dành cho game thủ, hiệu năng cao, tản nhiệt tốt	f	2025-01-10 09:15:00+00	2025-01-10 09:15:00+00
3	Mỏng nhẹ	Thiết kế mỏng nhẹ, di động, phù hợp cho doanh nhân	f	2025-01-10 10:45:00+00	2025-01-10 10:45:00+00
4	Đồ họa	Dành cho dân thiết kế, đồ họa, cấu hình mạnh	f	2025-01-10 14:20:00+00	2025-01-10 14:20:00+00
5	Cao cấp	Dòng cao cấp, thiết kế sang trọng, hiệu năng mạnh	f	2025-01-10 16:30:00+00	2025-01-10 16:30:00+00
6	2-in-1	Laptop có thể chuyển đổi thành tablet, màn hình cảm ứng, linh hoạt	f	2025-01-11 09:15:00+00	2025-01-11 09:15:00+00
7	Sinh viên	Laptop giá rẻ, cấu hình phù hợp cho học tập và giải trí cơ bản	f	2025-01-11 11:30:00+00	2025-01-11 11:30:00+00
\.


--
-- Data for Name: Cities; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."Cities" (id, "CityCode", "CityName", "createdAt", "updatedAt") FROM stdin;
1	HCM	Hồ Chí Minh	2025-01-03 09:00:00+00	2025-01-03 09:00:00+00
2	HAN	Hà Nội	2025-01-03 09:05:00+00	2025-01-03 09:05:00+00
3	DAN	Đà Nẵng	2025-01-03 09:10:00+00	2025-01-03 09:10:00+00
4	CTO	Cần Thơ	2025-01-03 09:15:00+00	2025-01-03 09:15:00+00
5	HAP	Hải Phòng	2025-01-03 09:20:00+00	2025-01-03 09:20:00+00
6	NTH	Nha Trang	2025-01-03 09:25:00+00	2025-01-03 09:25:00+00
7	BMT	Buôn Ma Thuột	2025-01-03 09:30:00+00	2025-01-03 09:30:00+00
8	HUE	Huế	2025-01-03 09:35:00+00	2025-01-03 09:35:00+00
9	VUT	Vũng Tàu	2025-01-03 09:40:00+00	2025-01-03 09:40:00+00
10	QNH	Quy Nhơn	2025-01-03 09:45:00+00	2025-01-03 09:45:00+00
11	TNH	Thái Nguyên	2025-01-03 09:50:00+00	2025-01-03 09:50:00+00
12	BDG	Bình Dương	2025-01-03 09:55:00+00	2025-01-03 09:55:00+00
13	DLT	Đà Lạt	2025-01-03 10:00:00+00	2025-01-03 10:00:00+00
14	PTO	Phan Thiết	2025-01-03 10:05:00+00	2025-01-03 10:05:00+00
15	PLK	Pleiku	2025-01-03 10:10:00+00	2025-01-03 10:10:00+00
16	HUI	Hưng Yên	2025-01-03 10:15:00+00	2025-01-03 10:15:00+00
17	BNH	Bắc Ninh	2025-01-03 10:20:00+00	2025-01-03 10:20:00+00
18	AGG	An Giang	2025-01-03 10:25:00+00	2025-01-03 10:25:00+00
19	BRV	Bà Rịa - Vũng Tàu	2025-01-03 10:30:00+00	2025-01-03 10:30:00+00
20	BGG	Bắc Giang	2025-01-03 10:35:00+00	2025-01-03 10:35:00+00
21	BKN	Bắc Kạn	2025-01-03 10:40:00+00	2025-01-03 10:40:00+00
22	BLU	Bạc Liêu	2025-01-03 10:45:00+00	2025-01-03 10:45:00+00
23	BTR	Bến Tre	2025-01-03 10:50:00+00	2025-01-03 10:50:00+00
24	BDH	Bình Định	2025-01-03 10:55:00+00	2025-01-03 10:55:00+00
25	BPC	Bình Phước	2025-01-03 11:00:00+00	2025-01-03 11:00:00+00
26	BTN	Bình Thuận	2025-01-03 11:05:00+00	2025-01-03 11:05:00+00
27	CMU	Cà Mau	2025-01-03 11:10:00+00	2025-01-03 11:10:00+00
28	CBG	Cao Bằng	2025-01-03 11:15:00+00	2025-01-03 11:15:00+00
29	DLK	Đắk Lắk	2025-01-03 11:20:00+00	2025-01-03 11:20:00+00
30	DNO	Đắk Nông	2025-01-03 11:25:00+00	2025-01-03 11:25:00+00
31	DBN	Điện Biên	2025-01-03 11:30:00+00	2025-01-03 11:30:00+00
32	DNI	Đồng Nai	2025-01-03 11:35:00+00	2025-01-03 11:35:00+00
33	DTP	Đồng Tháp	2025-01-03 11:40:00+00	2025-01-03 11:40:00+00
34	GLA	Gia Lai	2025-01-03 11:45:00+00	2025-01-03 11:45:00+00
35	HGG	Hà Giang	2025-01-03 11:50:00+00	2025-01-03 11:50:00+00
36	HNM	Hà Nam	2025-01-03 11:55:00+00	2025-01-03 11:55:00+00
37	HTH	Hà Tĩnh	2025-01-03 12:00:00+00	2025-01-03 12:00:00+00
38	HDG	Hải Dương	2025-01-03 12:05:00+00	2025-01-03 12:05:00+00
39	HGG	Hậu Giang	2025-01-03 12:10:00+00	2025-01-03 12:10:00+00
40	HBH	Hòa Bình	2025-01-03 12:15:00+00	2025-01-03 12:15:00+00
41	KHA	Khánh Hòa	2025-01-03 12:20:00+00	2025-01-03 12:20:00+00
42	KGG	Kiên Giang	2025-01-03 12:25:00+00	2025-01-03 12:25:00+00
43	KTM	Kon Tum	2025-01-03 12:30:00+00	2025-01-03 12:30:00+00
44	LCU	Lai Châu	2025-01-03 12:35:00+00	2025-01-03 12:35:00+00
45	LDG	Lâm Đồng	2025-01-03 12:40:00+00	2025-01-03 12:40:00+00
46	LSN	Lạng Sơn	2025-01-03 12:45:00+00	2025-01-03 12:45:00+00
47	LCI	Lào Cai	2025-01-03 12:50:00+00	2025-01-03 12:50:00+00
48	LAN	Long An	2025-01-03 12:55:00+00	2025-01-03 12:55:00+00
49	NDH	Nam Định	2025-01-03 13:00:00+00	2025-01-03 13:00:00+00
50	NAN	Nghệ An	2025-01-03 13:05:00+00	2025-01-03 13:05:00+00
51	NBH	Ninh Bình	2025-01-03 13:10:00+00	2025-01-03 13:10:00+00
52	NTN	Ninh Thuận	2025-01-03 13:15:00+00	2025-01-03 13:15:00+00
53	PTO	Phú Thọ	2025-01-03 13:20:00+00	2025-01-03 13:20:00+00
54	PYN	Phú Yên	2025-01-03 13:25:00+00	2025-01-03 13:25:00+00
55	QBH	Quảng Bình	2025-01-03 13:30:00+00	2025-01-03 13:30:00+00
56	QNM	Quảng Nam	2025-01-03 13:35:00+00	2025-01-03 13:35:00+00
57	QNI	Quảng Ngãi	2025-01-03 13:40:00+00	2025-01-03 13:40:00+00
58	QNH	Quảng Ninh	2025-01-03 13:45:00+00	2025-01-03 13:45:00+00
59	QTI	Quảng Trị	2025-01-03 13:50:00+00	2025-01-03 13:50:00+00
60	STG	Sóc Trăng	2025-01-03 13:55:00+00	2025-01-03 13:55:00+00
61	SLA	Sơn La	2025-01-03 14:00:00+00	2025-01-03 14:00:00+00
62	TNH	Tây Ninh	2025-01-03 14:05:00+00	2025-01-03 14:05:00+00
63	TBH	Thái Bình	2025-01-03 14:10:00+00	2025-01-03 14:10:00+00
64	THA	Thanh Hóa	2025-01-03 14:15:00+00	2025-01-03 14:15:00+00
65	TTH	Thừa Thiên Huế	2025-01-03 14:20:00+00	2025-01-03 14:20:00+00
66	TGG	Tiền Giang	2025-01-03 14:25:00+00	2025-01-03 14:25:00+00
67	TVH	Trà Vinh	2025-01-03 14:30:00+00	2025-01-03 14:30:00+00
68	TQG	Tuyên Quang	2025-01-03 14:35:00+00	2025-01-03 14:35:00+00
69	VLG	Vĩnh Long	2025-01-03 14:40:00+00	2025-01-03 14:40:00+00
70	VPC	Vĩnh Phúc	2025-01-03 14:45:00+00	2025-01-03 14:45:00+00
71	YBI	Yên Bái	2025-01-03 14:50:00+00	2025-01-03 14:50:00+00
\.


--
-- Data for Name: Customers; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."Customers" ("CustomerID", "FullName", "Email", "Phone", "Address", "CityCode", "IsDeleted", "createdAt", "updatedAt") FROM stdin;
1	Nguyễn Văn Anh	vananh@email.com	0901234567	123 Lê Lợi, Phường Bến Nghé, Quận 1	1	f	2025-01-15 09:30:00+00	2025-01-15 09:30:00+00
2	Trần Thị Mai	maitr@email.com	0912345678	45 Nguyễn Huệ, Phường Bến Nghé, Quận 1	1	f	2025-01-15 14:15:00+00	2025-01-15 14:15:00+00
3	Lê Hoàng Nam	namlh@email.com	0923456789	67 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1	1	f	2025-01-16 10:20:00+00	2025-01-16 10:20:00+00
4	Phạm Minh Tuấn	tuanpm@email.com	0934567890	89 Lý Tự Trọng, Hoàn Kiếm	2	f	2025-01-17 11:45:00+00	2025-01-17 11:45:00+00
5	Hoàng Thị Lan	lanht@email.com	0945678901	234 Trần Phú, Hải Châu	3	f	2025-01-18 13:30:00+00	2025-01-18 13:30:00+00
6	Vũ Đức Minh	minhvd@email.com	0956789012	56 Nguyễn Văn Linh, Hải Châu	3	f	2025-01-19 10:15:00+00	2025-01-19 10:15:00+00
7	Đặng Thu Hà	hadt@email.com	0967890123	78 Phan Chu Trinh, Ninh Kiều	4	f	2025-01-20 14:45:00+00	2025-01-20 14:45:00+00
8	Bùi Quang Huy	huybq@email.com	0978901234	90 Lê Duẩn, Hải An	5	f	2025-01-21 11:30:00+00	2025-01-21 11:30:00+00
9	Ngô Thị Thanh	thanhnt@email.com	0989012345	123 Trần Phú, Lộc Thọ	6	f	2025-01-22 09:20:00+00	2025-01-22 09:20:00+00
10	Đỗ Văn Hùng	hungdv@email.com	0990123456	45 Nguyễn Huệ, Thành phố Huế	8	f	2025-01-23 15:10:00+00	2025-01-23 15:10:00+00
11	Trần Đức Hiếu	hieutd@email.com	0912345987	28 Phan Xích Long, Phường 2, Phú Nhuận	1	f	2025-01-24 08:40:00+00	2025-01-24 08:40:00+00
12	Nguyễn Thị Kiều Trang	trangntk@email.com	0923456987	175 Tây Sơn, Đống Đa	2	f	2025-01-25 13:15:00+00	2025-01-25 13:15:00+00
13	Lê Minh Hoàng	hoanglm@email.com	0934567891	45 Nguyễn Thị Minh Khai, Hải Châu	3	f	2025-01-26 11:30:00+00	2025-01-26 11:30:00+00
14	Phạm Thanh Hương	huongpt@email.com	0945678902	101 Lê Hồng Phong, Ngô Quyền	5	f	2025-01-27 10:20:00+00	2025-01-27 10:20:00+00
15	Trịnh Văn Công	congtv@email.com	0956789198	65 Lê Duẩn, Quận 1	1	f	2025-01-28 14:40:00+00	2025-01-28 14:40:00+00
16	Nguyễn Thị Hồng Nhung	nhungnh@email.com	0967890234	42 Trương Định, Quận 3	1	f	2025-01-29 09:15:00+00	2025-01-29 09:15:00+00
17	Trần Quốc Bảo	baotq@email.com	0978234567	15 Lê Thánh Tôn, Quận Hoàn Kiếm	2	f	2025-01-29 14:30:00+00	2025-01-29 14:30:00+00
18	Đỗ Thị Kim Chi	chidtk@email.com	0989876543	76 Bạch Đằng, Quận Hải Châu	3	f	2025-01-30 10:45:00+00	2025-01-30 10:45:00+00
19	Lý Thanh Tùng	tunglt@email.com	0912876543	28 Võ Thị Sáu, Quận Ninh Kiều	4	f	2025-01-30 16:20:00+00	2025-01-30 16:20:00+00
20	Vũ Hoàng Giang	giangvh@email.com	0934567123	55 Trần Phú, Phường Lộc Thọ	6	f	2025-01-31 11:30:00+00	2025-01-31 11:30:00+00
\.


--
-- Data for Name: LaptopSpecs; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."LaptopSpecs" ("VariantID", "SKU", "LaptopID", "CPU", "GPU", "RAM", "Storage", "StorageType", "Color", "ImportPrice", "Price", "StockQuantity", "IsDeleted", "createdAt", "updatedAt") FROM stdin;
127681	LEN-12768-1-WHT	12768	Intel Core i7-1360P	Intel Iris Xe Graphics	16	512	SSD	White	100000000	120000000	15	f	2025-01-13 09:30:00+00	2025-01-13 09:30:00+00
127682	LEN-12768-2-BLK	12768	Intel Core i7-1360P	Intel Iris Xe Graphics	32	1024	SSD	Black	125000000	150000000	10	f	2025-01-13 10:15:00+00	2025-01-13 10:15:00+00
127683	LEN-12768-3-SLV	12768	Intel Core i7-1360P	Intel Iris Xe Graphics	16	2048	SSD	Silver	135000000	160000000	8	f	2025-01-13 11:00:00+00	2025-01-13 11:00:00+00
163541	LEN-16354-1-GRY	16354	Intel Core i7-14700HX	NVIDIA GeForce RTX 4070	16	1024	SSD	Storm Grey	145000000	170000000	20	f	2025-01-15 13:30:00+00	2025-01-15 13:30:00+00
163542	LEN-16354-2-GRY	16354	Intel Core i9-14900HX	NVIDIA GeForce RTX 4080	32	2048	SSD	Storm Grey	204000000	240000000	8	f	2025-01-15 14:15:00+00	2025-01-15 14:15:00+00
163543	LEN-16354-3-GRY	16354	Intel Core i9-14900HX	NVIDIA GeForce RTX 4090	64	4096	SSD	Storm Grey	263500000	310000000	5	f	2025-01-15 15:00:00+00	2025-01-15 15:00:00+00
183511	LEN-18351-1-BLU	18351	Intel Core i7-13700H	Intel Iris Xe Graphics	16	512	SSD	Storm Blue	93500000	110000000	12	f	2025-01-16 09:45:00+00	2025-01-16 09:45:00+00
183512	LEN-18351-2-BLU	18351	Intel Core i7-13700H	Intel Iris Xe Graphics	32	1024	SSD	Storm Blue	119000000	140000000	8	f	2025-01-16 10:30:00+00	2025-01-16 10:30:00+00
183513	LEN-18351-3-BLK	18351	Intel Core i9-13900H	Intel Iris Xe Graphics	32	2048	SSD	Obsidian Black	144500000	170000000	5	f	2025-01-16 11:15:00+00	2025-01-16 11:15:00+00
189501	MSI-18950-1-GRY	18950	AMD Ryzen 7 7840HS	AMD Radeon RX 7600S	16	512	SSD	Mecha Grey	102000000	120000000	25	f	2025-01-17 13:00:00+00	2025-01-17 13:00:00+00
189502	MSI-18950-2-GRY	18950	AMD Ryzen 9 7940HS	AMD Radeon RX 7700S	32	1024	SSD	Mecha Grey	136000000	160000000	15	f	2025-01-17 13:45:00+00	2025-01-17 13:45:00+00
189503	MSI-18950-3-BLK	18950	AMD Ryzen 9 7940HS	AMD Radeon RX 7700S	64	2048	SSD	Eclipse Black	178500000	210000000	7	f	2025-01-17 14:30:00+00	2025-01-17 14:30:00+00
193251	MSI-19325-1-SLV	19325	Intel Core i7-14700H	NVIDIA GeForce RTX 4060	16	1024	SSD	Lunar Silver	161500000	190000000	10	f	2025-01-18 09:20:00+00	2025-01-18 09:20:00+00
193252	MSI-19325-2-SLV	19325	Intel Core i9-14900H	NVIDIA GeForce RTX 4070	32	2048	SSD	Lunar Silver	204000000	240000000	5	f	2025-01-18 10:05:00+00	2025-01-18 10:05:00+00
199271	MSI-19927-1-GRY	19927	Intel Core i9-14900HX	NVIDIA GeForce RTX 4080	32	1024	SSD	Eclipse Gray	229500000	270000000	8	f	2025-01-19 14:00:00+00	2025-01-19 14:00:00+00
199272	MSI-19927-2-GRY	19927	Intel Core i9-14900HX	NVIDIA GeForce RTX 4090	64	2048	SSD	Eclipse Gray	289000000	340000000	5	f	2025-01-19 14:45:00+00	2025-01-19 14:45:00+00
202251	ASU-20225-1-WHT	20225	AMD Ryzen 9 7940HS	NVIDIA GeForce RTX 4070	16	1024	SSD	Moonlight White	153000000	180000000	15	f	2025-01-21 10:15:00+00	2025-01-21 10:15:00+00
202252	ASU-20225-2-GRY	20225	AMD Ryzen 9 7940HS	NVIDIA GeForce RTX 4080	32	2048	SSD	Eclipse Gray	195500000	230000000	10	f	2025-01-21 11:00:00+00	2025-01-21 11:00:00+00
202691	ASU-20269-1-BLK	20269	Intel Core i7-14700H	NVIDIA GeForce RTX 4060	16	512	SSD	Off Black	127500000	150000000	20	f	2025-01-22 14:30:00+00	2025-01-22 14:30:00+00
202692	ASU-20269-2-BLK	20269	Intel Core i9-14900H	NVIDIA GeForce RTX 4070	32	1024	SSD	Off Black	161500000	190000000	15	f	2025-01-22 15:15:00+00	2025-01-22 15:15:00+00
203531	DEL-20353-1-SLV	20353	Intel Core i7-13700H	NVIDIA RTX A2000	32	1024	SSD	Platinum Silver	187000000	220000000	10	f	2025-01-25 09:10:00+00	2025-01-25 09:10:00+00
203532	DEL-20353-2-SLV	20353	Intel Core i9-13900H	NVIDIA RTX A3000	64	2048	SSD	Platinum Silver	246500000	290000000	5	f	2025-01-25 09:55:00+00	2025-01-25 09:55:00+00
203881	LEN-20388-1-GRY	20388	Intel Core i5-13500H	NVIDIA GeForce RTX 3050	16	512	SSD	Onyx Gray	68000000	80000000	30	f	2025-01-26 13:20:00+00	2025-01-26 13:20:00+00
205771	HPP-20577-1-BLU	20577	Intel Core i5-14500H	NVIDIA GeForce RTX 4050	16	512	SSD	Performance Blue	76500000	90000000	25	f	2025-01-28 09:40:00+00	2025-01-28 09:40:00+00
205772	HPP-20577-2-BLU	20577	Intel Core i7-14700H	NVIDIA GeForce RTX 4060	32	1024	SSD	Performance Blue	110500000	130000000	15	f	2025-01-28 10:25:00+00	2025-01-28 10:25:00+00
206261	GGB-20626-1-BLK	20626	Intel Core i9-14900HX	NVIDIA GeForce RTX 4080	32	2048	SSD	Black	238000000	280000000	8	f	2025-01-29 14:15:00+00	2025-01-29 14:15:00+00
206262	GGB-20626-2-BLK	20626	Intel Core i9-14900HX	NVIDIA GeForce RTX 4090	64	4096	SSD	Black	306000000	360000000	5	f	2025-01-29 15:00:00+00	2025-01-29 15:00:00+00
206271	GGB-20627-1-BLK	20627	Intel Core i7-14700H	NVIDIA GeForce RTX 4060	16	1024	SSD	Black	119000000	140000000	20	f	2025-01-30 10:30:00+00	2025-01-30 10:30:00+00
206921	MSI-20692-1-BLK	20692	Intel Core i9-14900HX	NVIDIA GeForce RTX 4090	64	4096	SSD	Cosmic Black	416500000	490000000	3	f	2025-01-31 13:00:00+00	2025-01-31 13:00:00+00
207241	ASU-20724-1-BLK	20724	Intel Core i9-14900H	NVIDIA GeForce RTX 4080	32	1024	SSD	Off Black	212500000	250000000	12	f	2025-02-01 15:30:00+00	2025-02-01 15:30:00+00
207242	ASU-20724-2-BLK	20724	Intel Core i9-14900H	NVIDIA GeForce RTX 4090	64	2048	SSD	Off Black	272000000	320000000	8	f	2025-02-01 16:15:00+00	2025-02-01 16:15:00+00
209931	LEN-20993-1-GRY	20993	AMD Ryzen 7 7840U	AMD Radeon 780M	16	512	SSD	Storm Grey	68000000	80000000	25	f	2025-02-03 09:20:00+00	2025-02-03 09:20:00+00
209932	LEN-20993-2-GRY	20993	AMD Ryzen 9 7940U	AMD Radeon 780M	32	1024	SSD	Storm Grey	93500000	110000000	15	f	2025-02-03 10:05:00+00	2025-02-03 10:05:00+00
210371	HPP-21037-1-BLK	21037	Intel Core i7-14700H	NVIDIA GeForce RTX 4060	16	1024	SSD	Shadow Black	144500000	170000000	18	f	2025-02-04 11:00:00+00	2025-02-04 11:00:00+00
210372	HPP-21037-2-BLK	21037	Intel Core i9-14900H	NVIDIA GeForce RTX 4070	32	2048	SSD	Shadow Black	187000000	220000000	12	f	2025-02-04 11:45:00+00	2025-02-04 11:45:00+00
210421	ACR-21042-1-BLK	21042	Intel Core i7-14700H	NVIDIA GeForce RTX 4060	16	1024	SSD	Obsidian Black	136000000	160000000	15	f	2025-02-05 14:10:00+00	2025-02-05 14:10:00+00
210422	ACR-21042-2-BLK	21042	Intel Core i9-14900H	NVIDIA GeForce RTX 4070	32	2048	SSD	Obsidian Black	178500000	210000000	10	f	2025-02-05 14:55:00+00	2025-02-05 14:55:00+00
210561	HPP-21056-1-SLV	21056	Intel Core i5-14500H	NVIDIA GeForce RTX 4050	16	512	SSD	Mica Silver	76500000	90000000	25	f	2025-02-06 10:30:00+00	2025-02-06 10:30:00+00
210562	HPP-21056-2-SLV	21056	Intel Core i7-14700H	NVIDIA GeForce RTX 4060	32	1024	SSD	Mica Silver	110500000	130000000	15	f	2025-02-06 11:15:00+00	2025-02-06 11:15:00+00
211081	ACR-21108-1-BLK	21108	Intel Core i7-14700H	NVIDIA GeForce RTX 4070	32	1024	SSD	Abyssal Black	161500000	190000000	12	f	2025-02-07 13:40:00+00	2025-02-07 13:40:00+00
211082	ACR-21108-2-BLK	21108	Intel Core i9-14900H	NVIDIA GeForce RTX 4080	32	2048	SSD	Abyssal Black	204000000	240000000	8	f	2025-02-07 14:25:00+00	2025-02-07 14:25:00+00
211711	LEN-21171-1-OAT	21171	Intel Core i7-1360P	Intel Iris Xe Graphics	16	1024	SSD	Oatmeal	110500000	130000000	15	f	2025-02-09 10:00:00+00	2025-02-09 10:00:00+00
211712	LEN-21171-2-OAT	21171	Intel Core i7-1360P	Intel Iris Xe Graphics	32	2048	SSD	Oatmeal	136000000	160000000	10	f	2025-02-09 10:45:00+00	2025-02-09 10:45:00+00
211751	LEN-21175-1-GRY	21175	Intel Core i5-1340P	Intel Iris Xe Graphics	16	512	SSD	Storm Grey	85000000	100000000	20	f	2025-02-10 15:10:00+00	2025-02-10 15:10:00+00
211752	LEN-21175-2-GRY	21175	Intel Core i7-1360P	Intel Iris Xe Graphics	16	1024	SSD	Storm Grey	102000000	120000000	15	f	2025-02-10 15:55:00+00	2025-02-10 15:55:00+00
211831	LEN-21183-1-GRY	21183	AMD Ryzen 7 7840HS	NVIDIA GeForce RTX 4060	16	512	SSD	Onyx Grey	102000000	120000000	20	f	2025-02-12 09:10:00+00	2025-02-12 09:10:00+00
211832	LEN-21183-2-GRY	21183	AMD Ryzen 9 7940HS	NVIDIA GeForce RTX 4070	32	1024	SSD	Onyx Grey	136000000	160000000	15	f	2025-02-12 09:55:00+00	2025-02-12 09:55:00+00
212531	MSI-21253-1-BLK	21253	Intel Core i9-14900HX	NVIDIA GeForce RTX 4090	64	4096	SSD	Core Black	374000000	440000000	5	f	2025-02-13 13:40:00+00	2025-02-13 13:40:00+00
213021	MSI-21302-1-BLK	21302	Intel Core i5-13500H	NVIDIA GeForce RTX 4050	16	512	SSD	Black	76500000	90000000	25	f	2025-02-14 10:00:00+00	2025-02-14 10:00:00+00
213022	MSI-21302-2-BLK	21302	Intel Core i7-13700H	NVIDIA GeForce RTX 4060	16	1024	SSD	Black	102000000	120000000	20	f	2025-02-14 10:45:00+00	2025-02-14 10:45:00+00
213101	MSI-21310-1-BLK	21310	Intel Core i7-14700H	NVIDIA GeForce RTX 4060	16	1024	SSD	Black	110500000	130000000	18	f	2025-02-15 14:30:00+00	2025-02-15 14:30:00+00
213102	MSI-21310-2-BLK	21310	Intel Core i9-14900H	NVIDIA GeForce RTX 4070	32	2048	SSD	Black	153000000	180000000	12	f	2025-02-15 15:15:00+00	2025-02-15 15:15:00+00
213301	DEL-21330-1-GRY	21330	Intel Core i7-14700H	NVIDIA GeForce RTX 4060	16	512	SSD	Dark Shadow Grey	119000000	140000000	20	f	2025-02-17 09:20:00+00	2025-02-17 09:20:00+00
213302	DEL-21330-2-GRY	21330	Intel Core i7-14700H	NVIDIA GeForce RTX 4060	32	1024	SSD	Dark Shadow Grey	144500000	170000000	15	f	2025-02-17 10:05:00+00	2025-02-17 10:05:00+00
213401	DEL-21340-1-GRY	21340	Intel Core i5-1335U	Intel Iris Xe Graphics	16	512	SSD	Titan Grey	68000000	80000000	25	f	2025-02-18 13:30:00+00	2025-02-18 13:30:00+00
213451	LEN-21345-1-BLK	21345	Intel Core i7-1355U	Intel Iris Xe Graphics	16	512	SSD	Black	102000000	120000000	20	f	2025-02-19 11:00:00+00	2025-02-19 11:00:00+00
213452	LEN-21345-2-BLK	21345	Intel Core i7-1355U	Intel Iris Xe Graphics	32	1024	SSD	Black	127500000	150000000	15	f	2025-02-19 11:45:00+00	2025-02-19 11:45:00+00
213551	APL-21355-1-SLV	21355	Apple M4 Pro 14-Core	Apple M4 Pro 20-Core GPU	24	512	SSD	Silver	442000000	520000000	10	f	2025-02-21 09:30:00+00	2025-02-21 09:30:00+00
213552	APL-21355-2-SGY	21355	Apple M4 Pro 14-Core	Apple M4 Pro 20-Core GPU	32	1024	SSD	Space Gray	510000000	600000000	8	f	2025-02-21 10:15:00+00	2025-02-21 10:15:00+00
213601	APL-21360-1-SLV	21360	Apple M4 Max 14-Core	Apple M4 Max 32-Core GPU	36	1024	SSD	Silver	595000000	700000000	6	f	2025-02-23 14:00:00+00	2025-02-23 14:00:00+00
213602	APL-21360-2-SGY	21360	Apple M4 Max 14-Core	Apple M4 Max 38-Core GPU	48	2048	SSD	Space Gray	680000000	800000000	4	f	2025-02-23 14:45:00+00	2025-02-23 14:45:00+00
213651	ASU-21365-1-GRY	21365	Intel Core Ultra 9 185H	Intel Arc Graphics	32	1024	SSD	Meteor Grey	180000000	212000000	15	f	2025-02-25 10:30:00+00	2025-02-25 10:30:00+00
213652	ASU-21365-2-GRY	21365	Intel Core Ultra 9 185H	Intel Arc Graphics	64	2048	SSD	Meteor Grey	210000000	245000000	8	f	2025-02-25 11:15:00+00	2025-02-25 11:15:00+00
213701	DEL-21370-1-BLK	21370	Intel Core i7-12700H	NVIDIA GeForce RTX 3070 Ti	32	1024	SSD	Lunar Light	220000000	260000000	12	f	2025-02-26 13:30:00+00	2025-02-26 13:30:00+00
213702	DEL-21370-2-BLK	21370	Intel Core i9-12900HK	NVIDIA GeForce RTX 3080 Ti	64	2048	SSD	Dark Side of the Moon	280000000	330000000	6	f	2025-02-26 14:15:00+00	2025-02-26 14:15:00+00
213751	RZR-21375-1-BLK	21375	Intel Core i9-13950HX	NVIDIA GeForce RTX 4060	16	1024	SSD	Black	250000000	294000000	10	f	2025-02-27 10:30:00+00	2025-02-27 10:30:00+00
213752	RZR-21375-2-BLK	21375	Intel Core i9-13950HX	NVIDIA GeForce RTX 4070	32	1024	SSD	Black	270000000	320000000	8	f	2025-02-27 11:15:00+00	2025-02-27 11:15:00+00
\.


--
-- Data for Name: Laptops; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."Laptops" ("LaptopID", "BrandID", "CategoryID", "Picture", "ModelName", "ScreenSize", "OperatingSystem", "ReleaseYear", "Description", "Discount", "DiscountEndDate", "DiscountStartDate", "IsDeleted", "createdAt", "updatedAt") FROM stdin;
12768	6	3	/Assets/data/laptops/01.jpg	LG Gram 2023 16Z90RS	16	Windows 11	2023	Laptop siêu nhẹ với thời lượng pin ấn tượng	12000000	2025-02-15 23:59:59+00	2025-01-15 00:00:00+00	f	2025-01-12 09:15:00+00	2025-01-12 09:15:00+00
16354	3	2	/Assets/data/laptops/02.jpg	Lenovo Legion Pro 5 16IRX9 2024	16	Windows 11	2024	Laptop gaming cao cấp với hiệu năng mạnh mẽ	12000000	2025-02-20 23:59:59+00	2025-01-20 00:00:00+00	f	2025-01-13 10:30:00+00	2025-01-13 10:30:00+00
18351	3	3	/Assets/data/laptops/03.jpg	Lenovo Yoga 7 2-in-1 14IML9	14	Windows 11	2024	Laptop 2-in-1 linh hoạt với màn hình cảm ứng	13000000	2025-02-18 23:59:59+00	2025-01-18 00:00:00+00	f	2025-01-14 11:45:00+00	2025-01-14 11:45:00+00
18950	2	2	/Assets/data/laptops/04.jpg	Asus TUF Gaming A16 Advantage Edition FA617NT	16	Windows 11	2024	Laptop gaming bền bỉ với card đồ họa AMD	13000000	2025-02-15 23:59:59+00	2025-01-15 00:00:00+00	f	2025-01-14 14:20:00+00	2025-01-14 14:20:00+00
19325	1	2	/Assets/data/laptops/05.jpg	Dell Alienware X14 R2	14	Windows 11	2024	Laptop gaming mỏng nhẹ với thiết kế độc đáo	14000000	2025-02-25 23:59:59+00	2025-01-25 00:00:00+00	f	2025-01-15 09:10:00+00	2025-01-15 09:10:00+00
19927	2	2	/Assets/data/laptops/06.jpg	Asus ROG Strix G16 G614 Per Key RGB	16	Windows 11	2024	Laptop gaming cao cấp với bàn phím RGB từng phím	14000000	2025-02-28 23:59:59+00	2025-01-28 00:00:00+00	f	2025-01-16 10:30:00+00	2025-01-16 10:30:00+00
20225	2	2	/Assets/data/laptops/07.jpg	Asus ROG Zephyrus G14 GA403UV	14	Windows 11	2024	Laptop gaming nhỏ gọn với hiệu năng cao	15000000	2025-03-01 23:59:59+00	2025-02-01 00:00:00+00	f	2025-01-18 13:45:00+00	2025-01-18 13:45:00+00
20269	2	2	/Assets/data/laptops/08.jpg	Asus ROG Strix G16 G614JU	16	Windows 11	2024	Laptop gaming với màn hình lớn và tản nhiệt hiệu quả	15000000	2025-03-05 23:59:59+00	2025-02-05 00:00:00+00	f	2025-01-20 09:30:00+00	2025-01-20 09:30:00+00
20353	1	4	/Assets/data/laptops/09.jpg	Dell Mobile Precision Workstation 3581	15.6	Windows 11	2024	Laptop workstation chuyên nghiệp cho công việc đồ họa	16000000	2025-03-01 23:59:59+00	2025-02-01 00:00:00+00	f	2025-01-22 11:15:00+00	2025-01-22 11:15:00+00
20388	3	1	/Assets/data/laptops/10.jpg	Lenovo LOQ Essential 15IAX9E	15.6	Windows 11	2024	Laptop giá rẻ với hiệu năng ổn định	17000000	2025-02-15 23:59:59+00	2025-01-15 00:00:00+00	f	2025-01-23 14:40:00+00	2025-01-23 14:40:00+00
20577	4	2	/Assets/data/laptops/11.jpg	HP Victus 16 R0223TX	16	Windows 11	2024	Laptop gaming tầm trung với thiết kế hiện đại	27000000	2025-03-10 23:59:59+00	2025-02-10 00:00:00+00	f	2025-01-25 10:20:00+00	2025-01-25 10:20:00+00
20626	9	2	/Assets/data/laptops/12.jpg	Gigabyte AORUS 16X 9KG	16	Windows 11	2024	Laptop gaming cao cấp với màn hình 16 inch	21000000	2025-03-12 23:59:59+00	2025-02-12 00:00:00+00	f	2025-01-26 13:45:00+00	2025-01-26 13:45:00+00
20627	9	2	/Assets/data/laptops/13.jpg	Gigabyte G6 KF	15.6	Windows 11	2024	Laptop gaming với giá thành hợp lý	22000000	2025-02-20 23:59:59+00	2025-01-20 00:00:00+00	f	2025-01-27 09:10:00+00	2025-01-27 09:10:00+00
20692	8	2	/Assets/data/laptops/14.jpg	MSI Titan 18 HX AI A2XWJG 034VN Dragon Edition	18	Windows 11	2024	Laptop gaming cao cấp phiên bản đặc biệt Dragon Edition	22200000	2025-03-05 23:59:59+00	2025-02-05 00:00:00+00	f	2025-01-30 11:30:00+00	2025-01-30 11:30:00+00
20724	2	2	/Assets/data/laptops/15.jpg	Asus ROG Zephyrus G16 GA605	16	Windows 11	2024	Laptop gaming mỏng nhẹ với hiệu năng mạnh mẽ	21200000	2025-03-01 23:59:59+00	2025-02-01 00:00:00+00	f	2025-01-31 14:45:00+00	2025-01-31 14:45:00+00
20993	3	1	/Assets/data/laptops/16.jpg	Lenovo Ideapad 5 Xiaoxin Pro 14 AMD	14	Windows 11	2024	Laptop văn phòng với chip AMD tiết kiệm điện	21600000	2025-02-25 23:59:59+00	2025-01-25 00:00:00+00	f	2025-02-01 09:30:00+00	2025-02-01 09:30:00+00
21037	4	5	/Assets/data/laptops/17.jpg	HP Omen Transcend 14	14	Windows 11	2024	Laptop gaming cao cấp nhỏ gọn của HP	10000008	2025-03-10 23:59:59+00	2025-02-10 00:00:00+00	f	2025-02-02 10:15:00+00	2025-02-02 10:15:00+00
21042	7	5	/Assets/data/laptops/18.jpg	Acer Predator Helios Neo 14 PHN14 51	14	Windows 11	2024	Laptop gaming nhỏ gọn với thiết kế độc đáo	21800000	2025-03-15 23:59:59+00	2025-02-15 00:00:00+00	f	2025-02-04 13:20:00+00	2025-02-04 13:20:00+00
21056	4	2	/Assets/data/laptops/19.jpg	HP Victus 16 R1190TX	16	Windows 11	2024	Laptop gaming tầm trung với màn hình lớn	21800000	2025-03-01 23:59:59+00	2025-02-01 00:00:00+00	f	2025-02-05 09:45:00+00	2025-02-05 09:45:00+00
21108	7	5	/Assets/data/laptops/20.jpg	Acer Predator Helios Neo 16S AI	16	Windows 11	2024	Laptop gaming cao cấp tích hợp AI	23800000	2025-03-08 23:59:59+00	2025-02-08 00:00:00+00	f	2025-02-06 11:30:00+00	2025-02-06 11:30:00+00
21171	3	3	/Assets/data/laptops/21.jpg	Lenovo Yoga Slim 9 14ILL10	14	Windows 11	2024	Laptop cao cấp siêu mỏng nhẹ	23800000	2025-03-03 23:59:59+00	2025-02-03 00:00:00+00	f	2025-02-08 09:15:00+00	2025-02-08 09:15:00+00
21175	3	3	/Assets/data/laptops/22.jpg	Lenovo Yoga Slim 7 14ILL10	14	Windows 11	2024	Laptop mỏng nhẹ với hiệu năng ổn định	13800000	2025-02-25 23:59:59+00	2025-01-25 00:00:00+00	f	2025-02-09 14:20:00+00	2025-02-09 14:20:00+00
21183	3	2	/Assets/data/laptops/23.jpg	Lenovo Legion 5 R7000 APH9	15.6	Windows 11	2024	Laptop gaming với chip AMD mạnh mẽ	13800000	2025-03-12 23:59:59+00	2025-02-12 00:00:00+00	f	2025-02-10 10:00:00+00	2025-02-10 10:00:00+00
21253	8	2	/Assets/data/laptops/24.jpg	MSI Titan 18 HX AI A2XW	18	Windows 11	2024	Laptop gaming cao cấp với màn hình 18 inch	10000008	2025-03-18 23:59:59+00	2025-02-18 00:00:00+00	f	2025-02-11 13:10:00+00	2025-02-11 13:10:00+00
21302	8	2	/Assets/data/laptops/25.jpg	MSI Raider GE78 HX AI A2XW	17.3	Windows 11	2024	Laptop gaming cao cấp với công nghệ AI	14900000	2025-02-20 23:59:59+00	2025-01-20 00:00:00+00	f	2025-02-12 09:30:00+00	2025-02-12 09:30:00+00
21310	8	2	/Assets/data/laptops/26.jpg	MSI Katana A15 AI B8V	15.6	Windows 11	2024	Laptop gaming tầm trung với thiết kế gaming	14900000	2025-03-05 23:59:59+00	2025-02-05 00:00:00+00	f	2025-02-14 11:45:00+00	2025-02-14 11:45:00+00
21330	1	2	/Assets/data/laptops/27.jpg	Dell Gaming G15 5530 i7H165W11GR4060	15.6	Windows 11	2024	Laptop gaming Dell với thiết kế bền bỉ	10000015	2025-03-01 23:59:59+00	2025-02-01 00:00:00+00	f	2025-02-15 14:30:00+00	2025-02-15 14:30:00+00
21340	1	3	/Assets/data/laptops/28.jpg	Dell Latitude 3450	14	Windows 11	2024	Laptop doanh nhân với độ bền cao	10000012	2025-02-25 23:59:59+00	2025-01-25 00:00:00+00	f	2025-02-16 10:20:00+00	2025-02-16 10:20:00+00
21345	3	3	/Assets/data/laptops/29.jpg	Lenovo ThinkPad T14s Gen 5	14	Windows 11	2024	Laptop doanh nhân cao cấp với bảo mật tốt	12000000	2025-03-15 23:59:59+00	2025-02-15 00:00:00+00	f	2025-02-18 09:45:00+00	2025-02-18 09:45:00+00
21355	10	3	/Assets/data/laptops/30.jpg	MacBook Pro 2024 16 inch	16	macOS Sequoia	2024	Laptop cao cấp với chip Apple M4 Pro mạnh mẽ, màn hình Mini-LED XDR	25000000	2025-03-25 23:59:59+00	2025-02-25 00:00:00+00	f	2025-02-20 11:30:00+00	2025-02-20 11:30:00+00
21360	10	3	/Assets/data/laptops/31.jpg	MacBook Pro 2024 14 inch	14	macOS Sequoia	2024	Laptop cao cấp với chip Apple M4 Max siêu mạnh, màn hình Mini-LED XDR	30000000	2025-03-25 23:59:59+00	2025-02-25 00:00:00+00	f	2025-02-22 14:15:00+00	2025-02-22 14:15:00+00
21365	2	3	/Assets/data/laptops/32.jpg	Asus Zenbook Duo OLED UX8406MA-PZ142W	14	Windows 11	2024	Laptop cao cấp với màn hình OLED kép, CPU Core Ultra 9 185H và 32GB RAM	7000000	2025-03-28 23:59:59+00	2025-02-28 00:00:00+00	f	2025-02-24 10:15:00+00	2025-02-24 10:15:00+00
21370	1	2	/Assets/data/laptops/33.jpg	Dell Alienware x17 R2 Gaming	17.3	Windows 11	2024	Laptop gaming cao cấp với Intel Core i9-12900HK và NVIDIA GeForce RTX 3080 Ti	15000000	2025-03-30 23:59:59+00	2025-03-01 00:00:00+00	f	2025-02-25 11:30:00+00	2025-02-25 11:30:00+00
21375	5	2	/Assets/data/laptops/34.jpg	Razer Blade 16 (2023)	16	Windows 11	2023	Laptop gaming cao cấp với Intel Core i9 13950HX và NVIDIA GeForce RTX 4060	10500000	2025-04-05 23:59:59+00	2025-03-05 00:00:00+00	f	2025-02-26 14:45:00+00	2025-02-26 14:45:00+00
\.


--
-- Data for Name: OrderItems; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."OrderItems" (id, "OrderID", "VariantID", "Quantity", "UnitPrice", "createdAt", "updatedAt") FROM stdin;
1	1	127682	2	138000000	2025-02-01 09:30:00+00	2025-02-01 09:30:00+00
2	1	127681	1	108000000	2025-02-01 09:30:00+00	2025-02-01 09:30:00+00
3	1	127683	3	144000000	2025-02-01 09:30:00+00	2025-02-01 09:30:00+00
4	2	163542	1	228000000	2025-02-03 14:15:00+00	2025-02-03 14:15:00+00
5	2	163541	2	153000000	2025-02-03 14:15:00+00	2025-02-03 14:15:00+00
6	2	183512	2	63000000	2025-02-03 14:15:00+00	2025-02-03 14:15:00+00
7	2	203881	3	72000000	2025-02-03 14:15:00+00	2025-02-03 14:15:00+00
8	3	199272	1	326000000	2025-02-05 11:20:00+00	2025-02-05 11:20:00+00
9	3	199271	2	243000000	2025-02-05 11:20:00+00	2025-02-05 11:20:00+00
10	3	202691	3	67500000	2025-02-05 11:20:00+00	2025-02-05 11:20:00+00
11	3	210561	2	81000000	2025-02-05 11:20:00+00	2025-02-05 11:20:00+00
12	3	213021	1	81000000	2025-02-05 11:20:00+00	2025-02-05 11:20:00+00
13	4	183511	3	97000000	2025-02-07 16:45:00+00	2025-02-07 16:45:00+00
14	4	213401	2	69999988	2025-02-07 16:45:00+00	2025-02-07 16:45:00+00
15	4	183512	1	126000000	2025-02-07 16:45:00+00	2025-02-07 16:45:00+00
16	4	211751	2	90000000	2025-02-07 16:45:00+00	2025-02-07 16:45:00+00
17	5	206921	1	467800000	2025-02-10 10:00:00+00	2025-02-10 10:00:00+00
18	5	212531	1	396000000	2025-02-10 10:00:00+00	2025-02-10 10:00:00+00
19	5	206262	2	324000000	2025-02-10 10:00:00+00	2025-02-10 10:00:00+00
20	6	210421	3	138200000	2025-02-12 13:30:00+00	2025-02-12 13:30:00+00
21	6	209931	2	58400000	2025-02-12 13:30:00+00	2025-02-12 13:30:00+00
22	6	210422	1	189000000	2025-02-12 13:30:00+00	2025-02-12 13:30:00+00
23	6	209932	2	99000000	2025-02-12 13:30:00+00	2025-02-12 13:30:00+00
24	7	163541	3	158000000	2025-02-15 09:15:00+00	2025-02-15 09:15:00+00
25	7	202251	1	162000000	2025-02-15 09:15:00+00	2025-02-15 09:15:00+00
26	7	211081	2	171000000	2025-02-15 09:15:00+00	2025-02-15 09:15:00+00
27	7	211711	1	117000000	2025-02-15 09:15:00+00	2025-02-15 09:15:00+00
28	7	205771	3	81000000	2025-02-15 09:15:00+00	2025-02-15 09:15:00+00
29	8	202252	2	215000000	2025-02-17 11:45:00+00	2025-02-17 11:45:00+00
30	8	202251	1	162000000	2025-02-17 11:45:00+00	2025-02-17 11:45:00+00
31	8	202691	3	135000000	2025-02-17 11:45:00+00	2025-02-17 11:45:00+00
32	9	211832	2	146200000	2025-02-20 14:20:00+00	2025-02-20 14:20:00+00
33	9	211831	1	108000000	2025-02-20 14:20:00+00	2025-02-20 14:20:00+00
34	9	203881	3	72000000	2025-02-20 14:20:00+00	2025-02-20 14:20:00+00
35	9	213401	2	72000000	2025-02-20 14:20:00+00	2025-02-20 14:20:00+00
36	10	206261	1	259000000	2025-02-22 15:50:00+00	2025-02-22 15:50:00+00
37	10	206262	2	324000000	2025-02-22 15:50:00+00	2025-02-22 15:50:00+00
38	10	206271	3	126000000	2025-02-22 15:50:00+00	2025-02-22 15:50:00+00
39	10	210371	1	153000000	2025-02-22 15:50:00+00	2025-02-22 15:50:00+00
40	11	189501	2	108000000	2025-02-25 10:15:00+00	2025-02-25 10:15:00+00
41	11	189502	3	144000000	2025-02-25 10:15:00+00	2025-02-25 10:15:00+00
42	11	189503	1	189000000	2025-02-25 10:15:00+00	2025-02-25 10:15:00+00
43	11	203531	2	198000000	2025-02-25 10:15:00+00	2025-02-25 10:15:00+00
44	12	193251	3	171000000	2025-03-01 14:30:00+00	2025-03-01 14:30:00+00
45	12	193252	1	216000000	2025-03-01 14:30:00+00	2025-03-01 14:30:00+00
46	12	202692	2	171000000	2025-03-01 14:30:00+00	2025-03-01 14:30:00+00
47	13	203532	1	261000000	2025-03-05 09:45:00+00	2025-03-05 09:45:00+00
48	13	205772	3	117000000	2025-03-05 09:45:00+00	2025-03-05 09:45:00+00
49	13	210372	2	198000000	2025-03-05 09:45:00+00	2025-03-05 09:45:00+00
50	14	207241	2	225000000	2025-03-10 15:20:00+00	2025-03-10 15:20:00+00
51	14	207242	1	288000000	2025-03-10 15:20:00+00	2025-03-10 15:20:00+00
52	14	210562	3	117000000	2025-03-10 15:20:00+00	2025-03-10 15:20:00+00
53	14	211082	2	216000000	2025-03-10 15:20:00+00	2025-03-10 15:20:00+00
54	15	211752	3	108000000	2025-03-15 11:30:00+00	2025-03-15 11:30:00+00
55	15	213101	2	117000000	2025-03-15 11:30:00+00	2025-03-15 11:30:00+00
56	15	213102	1	162000000	2025-03-15 11:30:00+00	2025-03-15 11:30:00+00
57	16	213301	2	126000000	2025-03-18 10:45:00+00	2025-03-18 10:45:00+00
58	16	213302	3	153000000	2025-03-18 10:45:00+00	2025-03-18 10:45:00+00
59	16	213451	1	108000000	2025-03-18 10:45:00+00	2025-03-18 10:45:00+00
60	17	213452	3	135000000	2025-03-22 09:15:00+00	2025-03-22 09:15:00+00
61	17	183513	2	153000000	2025-03-22 09:15:00+00	2025-03-22 09:15:00+00
62	17	163543	1	279000000	2025-03-22 09:15:00+00	2025-03-22 09:15:00+00
63	18	213551	1	495000000	2025-03-23 09:30:00+00	2025-03-23 09:30:00+00
64	19	213552	1	570000000	2025-03-24 14:15:00+00	2025-03-24 14:15:00+00
65	20	213601	1	670000000	2025-03-25 10:20:00+00	2025-03-25 10:20:00+00
66	21	213602	1	770000000	2025-03-27 16:45:00+00	2025-03-27 16:45:00+00
67	21	213551	1	495000000	2025-03-27 16:45:00+00	2025-03-27 16:45:00+00
68	21	213552	1	255000000	2025-03-27 16:45:00+00	2025-03-27 16:45:00+00
\.


--
-- Data for Name: OrderStatuses; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."OrderStatuses" ("StatusID", "StatusName", "createdAt", "updatedAt") FROM stdin;
1	Pending	2025-01-09 13:00:00+00	2025-01-09 13:00:00+00
2	Processing	2025-01-09 13:05:00+00	2025-01-09 13:05:00+00
3	Shipped	2025-01-09 13:10:00+00	2025-01-09 13:10:00+00
4	Delivered	2025-01-09 13:15:00+00	2025-01-09 13:15:00+00
5	Cancelled	2025-01-09 13:20:00+00	2025-01-09 13:20:00+00
6	Refunded	2025-01-09 13:25:00+00	2025-01-09 13:25:00+00
\.


--
-- Data for Name: Orders; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."Orders" ("OrderID", "CustomerID", "OrderDate", "StatusID", "TotalAmount", "PaymentMethodID", "ShipCityID", "ShippingAddress", "ShippingCity", "ShippingPostalCode", "IsDeleted", "createdAt", "updatedAt") FROM stdin;
1	1	2025-02-01 09:30:00+00	4	390000000	2	1	123 Lê Lợi, Phường Bến Nghé, Quận 1	Hồ Chí Minh	700000	f	2025-02-01 09:30:00+00	2025-02-01 09:30:00+00
2	2	2025-02-03 14:15:00+00	3	579000000	1	1	45 Nguyễn Huệ, Phường Bến Nghé, Quận 1	Hồ Chí Minh	700000	f	2025-02-03 14:15:00+00	2025-02-03 14:15:00+00
3	4	2025-02-05 11:20:00+00	2	866000000	3	2	89 Lý Tự Trọng, Hoàn Kiếm	Hà Nội	100000	f	2025-02-05 11:20:00+00	2025-02-05 11:20:00+00
4	5	2025-02-07 16:45:00+00	1	382999988	7	3	234 Trần Phú, Hải Châu	Đà Nẵng	550000	f	2025-02-07 16:45:00+00	2025-02-07 16:45:00+00
5	7	2025-02-10 10:00:00+00	5	1187800000	5	4	78 Phan Chu Trinh, Ninh Kiều	Cần Thơ	900000	f	2025-02-10 10:00:00+00	2025-02-10 10:00:00+00
6	8	2025-02-12 13:30:00+00	2	484600000	4	5	90 Lê Duẩn, Hải An	Hải Phòng	180000	f	2025-02-12 13:30:00+00	2025-02-12 13:30:00+00
7	3	2025-02-15 09:15:00+00	1	689000000	8	1	67 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1	Hồ Chí Minh	700000	f	2025-02-15 09:15:00+00	2025-02-15 09:15:00+00
8	9	2025-02-17 11:45:00+00	2	512000000	6	6	123 Trần Phú, Lộc Thọ	Nha Trang	650000	f	2025-02-17 11:45:00+00	2025-02-17 11:45:00+00
9	10	2025-02-20 14:20:00+00	1	398200000	2	8	45 Nguyễn Huệ, Thành phố Huế	Huế	530000	f	2025-02-20 14:20:00+00	2025-02-20 14:20:00+00
10	6	2025-02-22 15:50:00+00	3	862000000	1	3	56 Nguyễn Văn Linh, Hải Châu	Đà Nẵng	550000	f	2025-02-22 15:50:00+00	2025-02-22 15:50:00+00
11	11	2025-02-25 10:15:00+00	1	639000000	2	1	28 Phan Xích Long, Phường 2, Phú Nhuận	Hồ Chí Minh	700000	f	2025-02-25 10:15:00+00	2025-02-25 10:15:00+00
12	12	2025-03-01 14:30:00+00	2	558000000	3	2	175 Tây Sơn, Đống Đa	Hà Nội	100000	f	2025-03-01 14:30:00+00	2025-03-01 14:30:00+00
13	13	2025-03-05 09:45:00+00	1	576000000	1	3	45 Nguyễn Thị Minh Khai, Hải Châu	Đà Nẵng	550000	f	2025-03-05 09:45:00+00	2025-03-05 09:45:00+00
14	14	2025-03-10 15:20:00+00	3	846000000	4	5	101 Lê Hồng Phong, Ngô Quyền	Hải Phòng	180000	f	2025-03-10 15:20:00+00	2025-03-10 15:20:00+00
15	15	2025-03-15 11:30:00+00	2	387000000	2	1	65 Lê Duẩn, Quận 1	Hồ Chí Minh	700000	f	2025-03-15 11:30:00+00	2025-03-15 11:30:00+00
16	11	2025-03-18 10:45:00+00	1	405000000	5	1	28 Phan Xích Long, Phường 2, Phú Nhuận	Hồ Chí Minh	700000	f	2025-03-18 10:45:00+00	2025-03-18 10:45:00+00
17	12	2025-03-22 09:15:00+00	2	567000000	1	2	175 Tây Sơn, Đống Đa	Hà Nội	100000	f	2025-03-22 09:15:00+00	2025-03-22 09:15:00+00
18	1	2025-03-23 09:30:00+00	1	495000000	5	1	123 Lê Lợi, Phường Bến Nghé, Quận 1	Hồ Chí Minh	700000	f	2025-03-23 09:30:00+00	2025-03-23 09:30:00+00
19	3	2025-03-24 14:15:00+00	2	570000000	1	1	67 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1	Hồ Chí Minh	700000	f	2025-03-24 14:15:00+00	2025-03-24 14:15:00+00
20	4	2025-03-25 10:20:00+00	3	670000000	2	2	89 Lý Tự Trọng, Hoàn Kiếm	Hà Nội	100000	f	2025-03-25 10:20:00+00	2025-03-25 10:20:00+00
21	13	2025-03-27 16:45:00+00	1	1520000000	3	3	45 Nguyễn Thị Minh Khai, Hải Châu	Đà Nẵng	550000	f	2025-03-27 16:45:00+00	2025-03-27 16:45:00+00
\.


--
-- Data for Name: PaymentMethods; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."PaymentMethods" ("PaymentMethodID", "MethodName", "createdAt", "updatedAt") FROM stdin;
1	Thẻ ATM nội địa	2025-01-08 11:00:00+00	2025-01-08 11:00:00+00
2	Ví MoMo	2025-01-08 11:05:00+00	2025-01-08 11:05:00+00
3	VNPay	2025-01-08 11:10:00+00	2025-01-08 11:10:00+00
4	ZaloPay	2025-01-08 11:15:00+00	2025-01-08 11:15:00+00
5	Thẻ tín dụng/ghi nợ	2025-01-08 11:20:00+00	2025-01-08 11:20:00+00
6	Chuyển khoản ngân hàng	2025-01-08 11:25:00+00	2025-01-08 11:25:00+00
7	Thanh toán khi nhận hàng (COD)	2025-01-08 11:30:00+00	2025-01-08 11:30:00+00
8	ShopeePay	2025-01-08 11:35:00+00	2025-01-08 11:35:00+00
\.


--
-- Data for Name: SequelizeMeta; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."SequelizeMeta" (name) FROM stdin;
20250320065551-create-category.js
20250320065554-create-brand.js
20250320065556-create-laptop.js
20250320065558-create-laptop-spec.js
20250320065601-create-city.js
20250320065603-create-customer.js
20250320065605-create-payment-method.js
20250320065608-create-order-status.js
20250320065610-create-order.js
20250320065612-create-order-item.js
20250320065818-create-software-version.js
\.


--
-- Data for Name: SoftwareVersion; Type: TABLE DATA; Schema: public; Owner: vietcq
--

COPY public."SoftwareVersion" ("Version", "createdAt", "updatedAt") FROM stdin;
3.2.1	2025-01-01 00:00:00+00	2025-01-01 00:00:00+00
\.


--
-- Name: Brands_BrandID_seq; Type: SEQUENCE SET; Schema: public; Owner: vietcq
--

SELECT pg_catalog.setval('public."Brands_BrandID_seq"', 1, false);


--
-- Name: Categories_CategoryID_seq; Type: SEQUENCE SET; Schema: public; Owner: vietcq
--

SELECT pg_catalog.setval('public."Categories_CategoryID_seq"', 1, false);


--
-- Name: Cities_id_seq; Type: SEQUENCE SET; Schema: public; Owner: vietcq
--

SELECT pg_catalog.setval('public."Cities_id_seq"', 1, false);


--
-- Name: Customers_CustomerID_seq; Type: SEQUENCE SET; Schema: public; Owner: vietcq
--

SELECT pg_catalog.setval('public."Customers_CustomerID_seq"', 1, false);


--
-- Name: LaptopSpecs_VariantID_seq; Type: SEQUENCE SET; Schema: public; Owner: vietcq
--

SELECT pg_catalog.setval('public."LaptopSpecs_VariantID_seq"', 1, false);


--
-- Name: Laptops_LaptopID_seq; Type: SEQUENCE SET; Schema: public; Owner: vietcq
--

SELECT pg_catalog.setval('public."Laptops_LaptopID_seq"', 1, false);


--
-- Name: OrderItems_id_seq; Type: SEQUENCE SET; Schema: public; Owner: vietcq
--

SELECT pg_catalog.setval('public."OrderItems_id_seq"', 68, true);


--
-- Name: OrderStatuses_StatusID_seq; Type: SEQUENCE SET; Schema: public; Owner: vietcq
--

SELECT pg_catalog.setval('public."OrderStatuses_StatusID_seq"', 1, false);


--
-- Name: Orders_OrderID_seq; Type: SEQUENCE SET; Schema: public; Owner: vietcq
--

SELECT pg_catalog.setval('public."Orders_OrderID_seq"', 1, false);


--
-- Name: PaymentMethods_PaymentMethodID_seq; Type: SEQUENCE SET; Schema: public; Owner: vietcq
--

SELECT pg_catalog.setval('public."PaymentMethods_PaymentMethodID_seq"', 1, false);


--
-- Name: Brands Brands_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Brands"
    ADD CONSTRAINT "Brands_pkey" PRIMARY KEY ("BrandID");


--
-- Name: Categories Categories_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Categories"
    ADD CONSTRAINT "Categories_pkey" PRIMARY KEY ("CategoryID");


--
-- Name: Cities Cities_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Cities"
    ADD CONSTRAINT "Cities_pkey" PRIMARY KEY (id);


--
-- Name: Customers Customers_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Customers"
    ADD CONSTRAINT "Customers_pkey" PRIMARY KEY ("CustomerID");


--
-- Name: LaptopSpecs LaptopSpecs_SKU_key; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."LaptopSpecs"
    ADD CONSTRAINT "LaptopSpecs_SKU_key" UNIQUE ("SKU");


--
-- Name: LaptopSpecs LaptopSpecs_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."LaptopSpecs"
    ADD CONSTRAINT "LaptopSpecs_pkey" PRIMARY KEY ("VariantID");


--
-- Name: Laptops Laptops_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Laptops"
    ADD CONSTRAINT "Laptops_pkey" PRIMARY KEY ("LaptopID");


--
-- Name: OrderItems OrderItems_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."OrderItems"
    ADD CONSTRAINT "OrderItems_pkey" PRIMARY KEY (id);


--
-- Name: OrderStatuses OrderStatuses_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."OrderStatuses"
    ADD CONSTRAINT "OrderStatuses_pkey" PRIMARY KEY ("StatusID");


--
-- Name: Orders Orders_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_pkey" PRIMARY KEY ("OrderID");


--
-- Name: PaymentMethods PaymentMethods_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."PaymentMethods"
    ADD CONSTRAINT "PaymentMethods_pkey" PRIMARY KEY ("PaymentMethodID");


--
-- Name: SequelizeMeta SequelizeMeta_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."SequelizeMeta"
    ADD CONSTRAINT "SequelizeMeta_pkey" PRIMARY KEY (name);


--
-- Name: Customers Customers_CityCode_fkey; Type: FK CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Customers"
    ADD CONSTRAINT "Customers_CityCode_fkey" FOREIGN KEY ("CityCode") REFERENCES public."Cities"(id);


--
-- Name: LaptopSpecs LaptopSpecs_LaptopID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."LaptopSpecs"
    ADD CONSTRAINT "LaptopSpecs_LaptopID_fkey" FOREIGN KEY ("LaptopID") REFERENCES public."Laptops"("LaptopID");


--
-- Name: Laptops Laptops_BrandID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Laptops"
    ADD CONSTRAINT "Laptops_BrandID_fkey" FOREIGN KEY ("BrandID") REFERENCES public."Brands"("BrandID");


--
-- Name: Laptops Laptops_CategoryID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Laptops"
    ADD CONSTRAINT "Laptops_CategoryID_fkey" FOREIGN KEY ("CategoryID") REFERENCES public."Categories"("CategoryID");


--
-- Name: OrderItems OrderItems_OrderID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."OrderItems"
    ADD CONSTRAINT "OrderItems_OrderID_fkey" FOREIGN KEY ("OrderID") REFERENCES public."Orders"("OrderID");


--
-- Name: OrderItems OrderItems_VariantID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."OrderItems"
    ADD CONSTRAINT "OrderItems_VariantID_fkey" FOREIGN KEY ("VariantID") REFERENCES public."LaptopSpecs"("VariantID");


--
-- Name: Orders Orders_CustomerID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_CustomerID_fkey" FOREIGN KEY ("CustomerID") REFERENCES public."Customers"("CustomerID");


--
-- Name: Orders Orders_PaymentMethodID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_PaymentMethodID_fkey" FOREIGN KEY ("PaymentMethodID") REFERENCES public."PaymentMethods"("PaymentMethodID");


--
-- Name: Orders Orders_ShipCityID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_ShipCityID_fkey" FOREIGN KEY ("ShipCityID") REFERENCES public."Cities"(id);


--
-- Name: Orders Orders_StatusID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_StatusID_fkey" FOREIGN KEY ("StatusID") REFERENCES public."OrderStatuses"("StatusID");


--
-- PostgreSQL database dump complete
--

