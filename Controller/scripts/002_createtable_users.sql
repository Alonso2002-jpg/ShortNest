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
    updated_at timestamp default CURRENT_TIMESTAMP not null
);

create table user_roles
(
    id        uuid
        constraint user_roles_pk
            primary key,
    user_id   uuid
        constraint user_roles_fk
            references users,
    name_role varchar(50)
);