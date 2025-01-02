--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2
-- Dumped by pg_dump version 17.2

-- Started on 2025-01-02 14:40:45

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
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
-- TOC entry 224 (class 1259 OID 16520)
-- Name: Battles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Battles" (
    "Id" integer NOT NULL,
    "Player1Id" integer,
    "Player2Id" integer,
    "Result" text,
    "Log" jsonb NOT NULL
);


ALTER TABLE public."Battles" OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 16519)
-- Name: Battles_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Battles_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Battles_Id_seq" OWNER TO postgres;

--
-- TOC entry 4894 (class 0 OID 0)
-- Dependencies: 223
-- Name: Battles_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Battles_Id_seq" OWNED BY public."Battles"."Id";


--
-- TOC entry 219 (class 1259 OID 16475)
-- Name: Cards; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Cards" (
    "Id" uuid NOT NULL,
    "Name" character varying(50) NOT NULL,
    "Damage" integer NOT NULL,
    "ElementType" character varying(20),
    "OwnerId" integer,
    "Locked" boolean DEFAULT false
);


ALTER TABLE public."Cards" OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 16487)
-- Name: Packages; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Packages" (
    "Id" integer NOT NULL,
    "Cards" jsonb NOT NULL
);


ALTER TABLE public."Packages" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16486)
-- Name: Packages_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Packages_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Packages_Id_seq" OWNER TO postgres;

--
-- TOC entry 4895 (class 0 OID 0)
-- Dependencies: 220
-- Name: Packages_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Packages_Id_seq" OWNED BY public."Packages"."Id";


--
-- TOC entry 222 (class 1259 OID 16495)
-- Name: Trades; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Trades" (
    "Id" uuid NOT NULL,
    "CardToTrade" uuid,
    "OwnerId" integer,
    "Type" character varying(20),
    "Damage" integer
);


ALTER TABLE public."Trades" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16461)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    "Id" integer NOT NULL,
    "Coins" integer DEFAULT 20,
    "Elo" integer DEFAULT 100,
    "Wins" integer DEFAULT 0,
    "Losses" integer DEFAULT 0,
    "Username" character varying(50) NOT NULL,
    "Password" character varying(200) NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16460)
-- Name: users_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."users_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."users_Id_seq" OWNER TO postgres;

--
-- TOC entry 4896 (class 0 OID 0)
-- Dependencies: 217
-- Name: users_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."users_Id_seq" OWNED BY public.users."Id";


--
-- TOC entry 4720 (class 2604 OID 16523)
-- Name: Battles Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Battles" ALTER COLUMN "Id" SET DEFAULT nextval('public."Battles_Id_seq"'::regclass);


--
-- TOC entry 4719 (class 2604 OID 16490)
-- Name: Packages Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages" ALTER COLUMN "Id" SET DEFAULT nextval('public."Packages_Id_seq"'::regclass);


--
-- TOC entry 4713 (class 2604 OID 16464)
-- Name: users Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN "Id" SET DEFAULT nextval('public."users_Id_seq"'::regclass);


--
-- TOC entry 4888 (class 0 OID 16520)
-- Dependencies: 224
-- Data for Name: Battles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Battles" ("Id", "Player1Id", "Player2Id", "Result", "Log") FROM stdin;
\.


--
-- TOC entry 4883 (class 0 OID 16475)
-- Dependencies: 219
-- Data for Name: Cards; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Cards" ("Id", "Name", "Damage", "ElementType", "OwnerId", "Locked") FROM stdin;
\.


--
-- TOC entry 4885 (class 0 OID 16487)
-- Dependencies: 221
-- Data for Name: Packages; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Packages" ("Id", "Cards") FROM stdin;
\.


--
-- TOC entry 4886 (class 0 OID 16495)
-- Dependencies: 222
-- Data for Name: Trades; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Trades" ("Id", "CardToTrade", "OwnerId", "Type", "Damage") FROM stdin;
\.


--
-- TOC entry 4882 (class 0 OID 16461)
-- Dependencies: 218
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users ("Id", "Coins", "Elo", "Wins", "Losses", "Username", "Password") FROM stdin;
\.


--
-- TOC entry 4897 (class 0 OID 0)
-- Dependencies: 223
-- Name: Battles_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Battles_Id_seq"', 1, false);


--
-- TOC entry 4898 (class 0 OID 0)
-- Dependencies: 220
-- Name: Packages_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Packages_Id_seq"', 1, false);


--
-- TOC entry 4899 (class 0 OID 0)
-- Dependencies: 217
-- Name: users_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."users_Id_seq"', 1, false);


--
-- TOC entry 4730 (class 2606 OID 16527)
-- Name: Battles Battles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Battles"
    ADD CONSTRAINT "Battles_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4724 (class 2606 OID 16480)
-- Name: Cards Cards_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4726 (class 2606 OID 16494)
-- Name: Packages Packages_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT "Packages_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4728 (class 2606 OID 16499)
-- Name: Trades Trades_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Trades"
    ADD CONSTRAINT "Trades_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4722 (class 2606 OID 16472)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY ("Id");


--
-- TOC entry 4734 (class 2606 OID 16528)
-- Name: Battles Player References; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Battles"
    ADD CONSTRAINT "Player References" FOREIGN KEY ("Player1Id") REFERENCES public.users("Id");


--
-- TOC entry 4731 (class 2606 OID 16481)
-- Name: Cards UsersId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "UsersId" FOREIGN KEY ("OwnerId") REFERENCES public.users("Id") ON DELETE CASCADE;


--
-- TOC entry 4732 (class 2606 OID 16500)
-- Name: Trades cardtotrade ref cardsid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Trades"
    ADD CONSTRAINT "cardtotrade ref cardsid" FOREIGN KEY ("CardToTrade") REFERENCES public."Cards"("Id");


--
-- TOC entry 4733 (class 2606 OID 16505)
-- Name: Trades ownerid ref user id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Trades"
    ADD CONSTRAINT "ownerid ref user id" FOREIGN KEY ("OwnerId") REFERENCES public.users("Id");


--
-- TOC entry 4735 (class 2606 OID 16533)
-- Name: Battles player ref; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Battles"
    ADD CONSTRAINT "player ref" FOREIGN KEY ("Player2Id") REFERENCES public.users("Id");


-- Completed on 2025-01-02 14:40:45

--
-- PostgreSQL database dump complete
--

