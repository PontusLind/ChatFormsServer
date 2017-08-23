create database pinocchio

go 

use pinocchio

go 

create table [login](
	ID int identity primary key not null,
	userName nvarchar(50) unique not null ,
	userpassword nvarchar(50) not null
)

go 

create procedure mysp_Logind
	@userName nvarchar(50),
	@userpassword nvarchar(50),
	@success bit out
as
Begin
	
	if exists(select 1 from [login] where userName = @userName and userpassword = @userpassword)
	set @success = 'true'

	else
	set @success = 'false'

End

go

create function UserExists
(
    @username nvarchar(50),
    @password nvarchar(50)
) returns bit
as
begin

declare
    @success bit = 'false'

    if exists(select 1 from [login] where username = @username and userpassword = @password)
    set @success = 'true'

    return @success
end

--select dbo.UserExists('Helen', '123345')

--insert into [login] (userName, userpassword) values ('Pontus', '123345'), ('Helen', '123345'), ('Freja', 'password')

--go

--create function CreateUser
--(
--    @username nvarchar(50),
--    @password nvarchar(50)
--) returns bit
--as
--begin

--declare
--    @success bit = 'false'

--    if exists(select 1 from [login] where username = @username )
--	begin
--    set @success = 'false'
--	end
--	else
--	begin
--	set @success = 'true'
--	insert into [login] (userName, userpassword) values (@username, @password)
--	end

--    return @success
--end

go

create procedure mysp_CreateUser
    @username nvarchar(50),
    @password nvarchar(50),
    @success bit out
as

begin

    if exists(select 1 from [Login] where username = @username)
    set @success = 'false'

    else
    set @success = 'true'
    insert into [Login] (username, userpassword) values (@username, @password)

end


