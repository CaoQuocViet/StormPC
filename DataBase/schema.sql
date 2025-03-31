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

