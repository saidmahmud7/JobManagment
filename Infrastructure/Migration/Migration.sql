create table Users
(
    userid    serial primary key,
    fullname  varchar(100),
    email     varchar(140) UNIQUE,
    phone     varchar(20) UNIQUE,
    role      varchar(100),
    createdAt date default current_date
);


CREATE TABLE jobs
(
    jobid       serial primary key,
    EmployerId  int REFERENCES Users (userid) on delete cascade,
    title       varchar(150),
    description text,
    salary      decimal check (salary > 0),
    country     varchar(100),
    city        varchar(100),
    status      varchar(50),
    createdAt   date default current_date,
    UpdatedAt   date default current_date
);

CREATE TABLE Applications
(
    ApplicationId serial PRIMARY key,
    JobId         int REFERENCES jobs (jobid) on delete cascade,
    ApplicantId   int REFERENCES users (userid) on delete cascade,
    Resume        text,
    Status        varchar(20),
    createdAt     date default current_date,
    UpdatedAt     date default current_date
);