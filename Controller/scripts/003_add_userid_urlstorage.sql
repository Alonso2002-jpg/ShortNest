alter table urlstorages
    add userid uuid;

alter table urlstorages
    add constraint urlstorages_user_fk
        foreign key (userid) references users;