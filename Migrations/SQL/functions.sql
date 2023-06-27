create or replace function get_by_client("SearchClientId" int, "SearchCpId" int default 0)
    returns table("Date" date, "Name" text, "Action" text, "DayOfWeek" int)
    language plpgsql
as
$$
begin
    if "SearchCpId" = 0 then
        return query
            select
                c."CreateDate"::date as "Date",
                cp."Name",
                c."Action",
                c."DayOfWeek"
            from "ClientToCateringPoint" as c
                     join "CateringPoint" CP on CP."Id" = c."CateringPointId"
            where "ClientId" = "SearchClientId"
              and "IsDeleted" = false
            order by c."CreateDate" desc;
    else
        return query
            select
                c."CreateDate"::date as "Date",
                cp."Name",
                c."Action",
                c."DayOfWeek"
            from "ClientToCateringPoint" as c
                     join "CateringPoint" CP on CP."Id" = c."CateringPointId"
            where "ClientId" = "SearchClientId"
              and "CateringPointId" = "SearchCpId"
              and "IsDeleted" = false
            order by c."CreateDate" desc;
    end if;
end;
$$;

create or replace function get_by_cp("SearchCpId" int, "SearchClientId" int default 0)
    returns table("Date" date, "Name" text, "Action" text, "DayOfWeek" int)
    language plpgsql
as
$$
begin
    if "SearchClientId" = 0 then
        return query
            select
                c."CreateDate"::date as "Date",
                CL."Name",
                c."Action",
                c."DayOfWeek"
            from "ClientToCateringPoint" as c
                     join "Client" CL on CL."Id" = c."ClientId"
            where "CateringPointId" = "SearchCpId"
            order by c."CreateDate" desc;
    else
        return query
            select
                c."CreateDate"::date as "Date",
                CL."Name",
                c."Action",
                c."DayOfWeek"
            from "ClientToCateringPoint" as c
                     join "Client" CL on CL."Id" = c."ClientId"
            where "CateringPointId" = "SearchCpId"
              and "ClientId" = "SearchClientId"
              and "IsDeleted" = false
            order by c."CreateDate" desc;
    end if;
end;
$$;