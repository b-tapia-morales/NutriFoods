CREATE USER nutrifoods_dev WITH ENCRYPTED PASSWORD 'MVmYneLqe91$' SUPERUSER;
GRANT ALL PRIVILEGES ON DATABASE nutrifoods_db TO nutrifoods_dev;
ALTER USER nutrifoods_dev WITH SUPERUSER;
