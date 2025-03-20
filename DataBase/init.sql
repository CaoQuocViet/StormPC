-- Grant all privileges on all tables in public schema
ALTER USER vietcq WITH SUPERUSER;

-- Update pg_hba.conf to allow all connections for the user
ALTER SYSTEM SET listen_addresses TO '*';

-- Create the database if it doesn't exist
CREATE DATABASE laptop_store;

-- Connect to the laptop_store database
\c laptop_store

-- Grant all privileges on all tables
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO vietcq;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO vietcq;
GRANT ALL PRIVILEGES ON ALL FUNCTIONS IN SCHEMA public TO vietcq;

-- Allow user to create new tables
GRANT CREATE ON SCHEMA public TO vietcq;

-- Add trust authentication for the user from any host
ALTER SYSTEM SET password_encryption TO 'scram-sha-256'; 