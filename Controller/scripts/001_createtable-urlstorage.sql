create table urlstorages
(
    id          uuid
        constraint urlstorage_pk
            primary key,
    urlreal     varchar(250),
    urlshortest varchar(250)
        constraint urlstorage_unq_2
            unique
);

