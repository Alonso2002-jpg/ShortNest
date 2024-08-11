alter table urlstorages
    add withpass bool default false not null;

alter table urlstorages
alter column urlreal type varchar(600) using urlreal::varchar(500);

alter table urlstorages
    add sitepass varchar(250);