﻿CREATE DATABASE nutrifoods_db;
CREATE USER nutrifoods_dev WITH ENCRYPTED PASSWORD 'MVmYneLqe91$';
GRANT ALL PRIVILEGES ON DATABASE nutrifoods_db TO nutrifoods_dev;
GRANT USAGE ON SCHEMA nutrifoods TO nutrifoods_dev;
GRANT SELECT ON ALL TABLES IN SCHEMA nutrifoods TO nutrifoods_dev;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA nutrifoods TO nutrifoods_dev;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA nutrifoods TO nutrifoods_dev;
