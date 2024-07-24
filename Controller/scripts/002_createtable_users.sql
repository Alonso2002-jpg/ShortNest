create table users
(
    id       uuid
        constraint users_pk
            primary key,
    name varchar(255) not null,
    lastname varchar(255) not null,
    email varchar(255) not null,
    username varchar(255) not null,
    password varchar(255) not null,
    createdat timestamp default CURRENT_TIMESTAMP not null,
    updatedat timestamp default CURRENT_TIMESTAMP not null
);

create table userroles
(
    id        uuid
        constraint user_roles_pk
            primary key,
    userid   uuid
        constraint user_roles_fk
            references users not null,
    rolename varchar(50) 
                constraint check_role_name
                     check ( rolename in ('ADMIN', 'USER') )
);