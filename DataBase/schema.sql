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
    id integer NOT NULL,
    "BrandID" integer,
    "BrandName" character varying(255),
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."Brands" OWNER TO vietcq;

--
-- Name: Brands_id_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Brands_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Brands_id_seq" OWNER TO vietcq;

--
-- Name: Brands_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Brands_id_seq" OWNED BY public."Brands".id;


--
-- Name: Categories; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."Categories" (
    id integer NOT NULL,
    "CategoryID" integer,
    "CategoryName" character varying(255),
    "Description" text,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."Categories" OWNER TO vietcq;

--
-- Name: Categories_id_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Categories_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Categories_id_seq" OWNER TO vietcq;

--
-- Name: Categories_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Categories_id_seq" OWNED BY public."Categories".id;


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
    id integer NOT NULL,
    "CustomerID" integer,
    "FullName" character varying(255),
    "Email" character varying(255),
    "Phone" character varying(255),
    "Address" text,
    "CityCode" character varying(3) NOT NULL,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."Customers" OWNER TO vietcq;

--
-- Name: Customers_id_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Customers_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Customers_id_seq" OWNER TO vietcq;

--
-- Name: Customers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Customers_id_seq" OWNED BY public."Customers".id;


--
-- Name: LaptopSpecs; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."LaptopSpecs" (
    id integer NOT NULL,
    "VariantID" character varying(255),
    "LaptopID" character varying(255),
    "CPU" character varying(255),
    "GPU" character varying(255),
    "RAM" integer,
    "Storage" integer,
    "StorageType" character varying(255),
    "Color" character varying(255),
    "Price" numeric,
    "StockQuantity" integer,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."LaptopSpecs" OWNER TO vietcq;

--
-- Name: LaptopSpecs_id_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."LaptopSpecs_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."LaptopSpecs_id_seq" OWNER TO vietcq;

--
-- Name: LaptopSpecs_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."LaptopSpecs_id_seq" OWNED BY public."LaptopSpecs".id;


--
-- Name: Laptops; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."Laptops" (
    id integer NOT NULL,
    "LaptopID" character varying(255),
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
-- Name: Laptops_id_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Laptops_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Laptops_id_seq" OWNER TO vietcq;

--
-- Name: Laptops_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Laptops_id_seq" OWNED BY public."Laptops".id;


--
-- Name: OrderItems; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."OrderItems" (
    id integer NOT NULL,
    "OrderID" integer,
    "VariantID" character varying(255),
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
    id integer NOT NULL,
    "StatusID" integer,
    "StatusName" character varying(255),
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."OrderStatuses" OWNER TO vietcq;

--
-- Name: OrderStatuses_id_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."OrderStatuses_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."OrderStatuses_id_seq" OWNER TO vietcq;

--
-- Name: OrderStatuses_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."OrderStatuses_id_seq" OWNED BY public."OrderStatuses".id;


--
-- Name: Orders; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."Orders" (
    id integer NOT NULL,
    "OrderID" integer,
    "CustomerID" integer,
    "OrderDate" timestamp with time zone,
    "StatusID" integer,
    "TotalAmount" numeric,
    "PaymentMethodID" integer,
    "ShipCityCode" character varying(255),
    "ShippingAddress" text,
    "ShippingCity" character varying(255),
    "ShippingPostalCode" character varying(10),
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."Orders" OWNER TO vietcq;

--
-- Name: Orders_id_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."Orders_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Orders_id_seq" OWNER TO vietcq;

--
-- Name: Orders_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."Orders_id_seq" OWNED BY public."Orders".id;


--
-- Name: PaymentMethods; Type: TABLE; Schema: public; Owner: vietcq
--

CREATE TABLE public."PaymentMethods" (
    id integer NOT NULL,
    "PaymentMethodID" integer,
    "MethodName" character varying(255),
    "createdAt" timestamp with time zone,
    "updatedAt" timestamp with time zone
);


ALTER TABLE public."PaymentMethods" OWNER TO vietcq;

--
-- Name: PaymentMethods_id_seq; Type: SEQUENCE; Schema: public; Owner: vietcq
--

CREATE SEQUENCE public."PaymentMethods_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."PaymentMethods_id_seq" OWNER TO vietcq;

--
-- Name: PaymentMethods_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vietcq
--

ALTER SEQUENCE public."PaymentMethods_id_seq" OWNED BY public."PaymentMethods".id;


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
-- Name: Brands id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Brands" ALTER COLUMN id SET DEFAULT nextval('public."Brands_id_seq"'::regclass);


--
-- Name: Categories id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Categories" ALTER COLUMN id SET DEFAULT nextval('public."Categories_id_seq"'::regclass);


--
-- Name: Cities id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Cities" ALTER COLUMN id SET DEFAULT nextval('public."Cities_id_seq"'::regclass);


--
-- Name: Customers id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Customers" ALTER COLUMN id SET DEFAULT nextval('public."Customers_id_seq"'::regclass);


--
-- Name: LaptopSpecs id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."LaptopSpecs" ALTER COLUMN id SET DEFAULT nextval('public."LaptopSpecs_id_seq"'::regclass);


--
-- Name: Laptops id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Laptops" ALTER COLUMN id SET DEFAULT nextval('public."Laptops_id_seq"'::regclass);


--
-- Name: OrderItems id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."OrderItems" ALTER COLUMN id SET DEFAULT nextval('public."OrderItems_id_seq"'::regclass);


--
-- Name: OrderStatuses id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."OrderStatuses" ALTER COLUMN id SET DEFAULT nextval('public."OrderStatuses_id_seq"'::regclass);


--
-- Name: Orders id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Orders" ALTER COLUMN id SET DEFAULT nextval('public."Orders_id_seq"'::regclass);


--
-- Name: PaymentMethods id; Type: DEFAULT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."PaymentMethods" ALTER COLUMN id SET DEFAULT nextval('public."PaymentMethods_id_seq"'::regclass);


--
-- Name: Brands Brands_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Brands"
    ADD CONSTRAINT "Brands_pkey" PRIMARY KEY (id);


--
-- Name: Categories Categories_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Categories"
    ADD CONSTRAINT "Categories_pkey" PRIMARY KEY (id);


--
-- Name: Cities Cities_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Cities"
    ADD CONSTRAINT "Cities_pkey" PRIMARY KEY (id);


--
-- Name: Customers Customers_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Customers"
    ADD CONSTRAINT "Customers_pkey" PRIMARY KEY (id);


--
-- Name: LaptopSpecs LaptopSpecs_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."LaptopSpecs"
    ADD CONSTRAINT "LaptopSpecs_pkey" PRIMARY KEY (id);


--
-- Name: Laptops Laptops_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Laptops"
    ADD CONSTRAINT "Laptops_pkey" PRIMARY KEY (id);


--
-- Name: OrderItems OrderItems_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."OrderItems"
    ADD CONSTRAINT "OrderItems_pkey" PRIMARY KEY (id);


--
-- Name: OrderStatuses OrderStatuses_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."OrderStatuses"
    ADD CONSTRAINT "OrderStatuses_pkey" PRIMARY KEY (id);


--
-- Name: Orders Orders_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_pkey" PRIMARY KEY (id);


--
-- Name: PaymentMethods PaymentMethods_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."PaymentMethods"
    ADD CONSTRAINT "PaymentMethods_pkey" PRIMARY KEY (id);


--
-- Name: SequelizeMeta SequelizeMeta_pkey; Type: CONSTRAINT; Schema: public; Owner: vietcq
--

ALTER TABLE ONLY public."SequelizeMeta"
    ADD CONSTRAINT "SequelizeMeta_pkey" PRIMARY KEY (name);


--
-- PostgreSQL database dump complete
--

