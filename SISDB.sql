create database SISDB;
use SISDB;

create table Students(
  student_id Int Primary Key Identity(1,1),
  first_name varchar(20) Not Null,
  last_name varchar(20) ,
  date_of_birth date not null,
  email varchar(100) not null,
  phone_number VARCHAR(15));

create table Teacher(
  teacher_id Int Primary Key Identity(1,1),
  first_name varchar(20) Not Null,
  last_name varchar(20),
  email varchar(100) not null );

create table Courses(
  course_id Int Primary Key Identity(1,1),
  course_name varchar(20) Not Null,
  credits TINYINT Not Null,
  teacher_id Int Not Null,
  CONSTRAINT fk_Cou_tec Foreign Key (teacher_id) References Teacher(teacher_id) On Delete CasCade On Update CasCade	);

create table Enrollments(
  enrollment_id Int Primary Key  Identity(1,1),
  student_id Int Not Null,
  course_id Int Not Null,
  enrollment_date date DEFAULT CAST(GETDATE() AS DATE) ,
  CONSTRAINT fk_Enr_Stu Foreign Key (student_id) References Students(student_id) On Delete CasCade On Update CasCade	,
  CONSTRAINT fk_Enr_Cou Foreign Key (course_id) References Courses(course_id) On Delete CasCade On Update CasCade	);

create table Payments(
  payment_id Int Primary Key  Identity(1,1),
  student_id Int Not Null,
  amount Decimal(10,2) default 10.00,
  payment_date date Not Null,
  CONSTRAINT fk_Pay_Stu Foreign Key (student_id) References Students(student_id) On Delete CasCade On Update CasCade	
  );


  -- Inserting Values--

  INSERT INTO Students (first_name, last_name, date_of_birth, email, phone_number) VALUES
('Aarav', 'Sharma', '2001-05-15', 'aarav.sharma@example.com', '9876543210'),
('Vihaan', 'Patel', '2000-11-22', 'vihaan.patel@example.com', '8765432109'),
('Anaya', 'Reddy', '2002-03-10', 'anaya.reddy@example.com', '7654321098'),
('Saanvi', 'Desai', '1999-08-30', 'saanvi.desai@example.com', '6543210987'),
('Kabir', 'Mehta', '2003-01-18', 'kabir.mehta@example.com', '5432109876'),
('Isha', 'Bansal', '1998-07-05', 'isha.bansal@example.com', '4321098765'),
('Arjun', 'Ghosh', '2001-12-12', 'arjun.ghosh@example.com', '3210987654'),
('Riya', 'Kumar', '2002-04-24', 'riya.kumar@example.com', '2109876543'),
('Dev', 'Nair', '2000-06-29', 'dev.nair@example.com', '1098765432'),
('Neha', 'Kapoor', '1997-10-14', 'neha.kapoor@example.com', '0987654321');


INSERT INTO Teacher (first_name, last_name, email) VALUES
('Rajesh', 'Singh', 'rajes.singh@example.com'),
('Priya', 'Nair', 'priy.nair@example.com'),
('Vikram', 'Rao', 'vikram.rao@example.com'),
('Aditi', 'Sharma', 'aditi.sharma@example.com'),
('Karan', 'Verma', 'karan.verma@example.com'),
('Sneha', 'Patel', 'sneha.patel@example.com'),
('Anil', 'Choudhary', 'anil.choudhary@example.com'),
('Geeta', 'Reddy', 'geeta.reddy@example.com'),
('Manoj', 'Yadav', 'manoj.yadav@example.com'),
('Tara', 'Bansal', 'tara.bansal@example.com');


INSERT INTO Courses (course_name, credits, teacher_id) VALUES
('Mathematics', 3, 1),
('English Literature', 4, 2),
('Computer Science', 4, 3),
('Physics', 4, 4),
('Chemistry', 3, 5),
('Biology', 3, 6),
('History', 2, 7),
('Geography', 3, 8),
('Economics', 4, 9),
('Political Science', 2, 10),
('Advanced Mathematics', 3, 1),
('Discrete Mathematics', 4, 1),
('Linear Algebra', 3, 1),
('Statistics', 4, 2),
('Calculus', 3, 2),
('Computer Networks', 4, 3),
('Database Management', 4, 3);



INSERT INTO Enrollments (student_id, course_id, enrollment_date) VALUES
(1, 1, '2023-01-20'),
(2, 2, '2023-01-22'),
(3, 3, '2023-01-25'),
(4, 4, '2023-01-27'),
(5, 5, '2023-01-30'),
(6, 6, '2023-02-01'),
(7, 7, '2023-02-03'),
(8, 8, '2023-02-05'),
(9, 9, '2023-02-08'),
(10, 10, '2023-02-10');


INSERT INTO Payments (student_id, amount, payment_date) VALUES
(1, 1000.00, '2023-03-01'),
(2, 1500.00, '2023-03-03'),
(3, 1200.00, '2023-03-05'),
(4, 1300.00, '2023-03-07'),
(5, 1400.00, '2023-03-09'),
(6, 1100.00, '2023-03-11'),
(7, 1150.00, '2023-03-13'),
(8, 1250.00, '2023-03-15'),
(9, 1350.00, '2023-03-17'),
(10, 1450.00, '2023-03-19');

INSERT INTO Teacher (first_name, last_name, email) VALUES
('Rajesh', 'Singh', 'rajesh.singh@example.com'),
('Priya', 'Nair', 'priya.nair@example.com');

INSERT INTO Enrollments (student_id, course_id, enrollment_date) VALUES
(1, 1, '2023-01-20'), 
(1, 2, '2023-01-22'),  
(2, 1, '2023-01-25'), 
(3, 3, '2023-01-27'),  
(1, 3, '2023-01-30');  


INSERT INTO Payments (student_id, amount, payment_date) VALUES
(1, 1000.00, '2023-03-01'),  
(1, 1500.00, '2023-04-01'),  
(2, 1500.00, '2023-03-03'),  
(3, 1200.00, '2023-03-05'); 



select* from students;
select * from teacher;
select * from Courses;
select * from Enrollments;
select * from Payments;

-- Task 2-------------------------------------------------------------------------------------------------------------------------


--1. Write an SQL query to insert a new student into the "Students" table with the following details:

Insert into SISDB.dbo.Students(first_name,last_name,date_of_birth,email,phone_number) values
     ('Jhon','Doe','1995-08-15','jhon.doe@example.com','1234567890');

--2 . Write an SQL query to enroll a student in a course. Choose an existing student and course and
--insert a record into the "Enrollments" table with the enrollment date

Insert into SISDB.dbo.Enrollments(student_id,course_id) values (11,7);

--.3 Update the email address of a specific teacher in the "Teacher" table. Choose any teacher and
--modify their email address.

Update Teacher set email='sneha.patale@changed.com' where teacher_id=6;

--4 Write an SQL query to delete a specific enrollment record from the "Enrollments" table. Select
--an enrollment record based on the student and course.

Delete from Enrollments where student_id=2 And course_id=3;

--.5 Update the "Courses" table to assign a specific teacher to a course. Choose any course and
--teacher from the respective tables.
 
 UPDATE Courses Set teacher_id = 2 where course_id = 1;
 
--6 Delete a specific student from the "Students" table and remove all their enrollment records
--from the "Enrollments" table. Be sure to maintain referential integrity

 
  Delete from Students  where student_id=2 ;

--7 Update the payment amount for a specific payment record in the "Payments" table. Choose any
--payment record and modify the payment amount.

Update Payments Set amount=1000.999 Where payment_id=9;

select*from Payments where student_id=1;



/*Task 3. Aggregate functions, Having, Order By, GroupBy and Joins: -----------------------------------------------------------------------------------------------------------------------



1. Write an SQL query to calculate the total payments made by a specific student. You will need to
join the "Payments" table with the "Students" table based on the student's ID.*/



Select Student_id ,sum(amount) as Total from Payments group by student_id;

/* 2. Write an SQL query to retrieve a list of courses along with the count of students enrolled in each
course. Use a JOIN operation between the "Courses" table and the "Enrollments" table. */

Select course_name , COUNT(student_id) as Student_Count from Courses c
Left Join Enrollments e on c.course_id=e.course_id Group By c.course_name;

/*3. Write an SQL query to find the names of students who have not enrolled in any course. Use a
LEFT JOIN between the "Students" table and the "Enrollments" table to identify students
without enrollments.*/

Select CONCAT_WS(' ',s.first_name,s.last_name) as Student_Name from Students s
Left Join Enrollments e On s.student_id=e.student_id 
where e.student_id is Null;

Select student_id from Enrollments  group by student_id order by student_id asc;

/* 4. Write an SQL query to retrieve the first name, last name of students, and the names of the
courses they are enrolled in. Use JOIN operations between the "Students" table and the
"Enrollments" and "Courses" tables.
*/

Select 
    s.first_name, 
    s.last_name, 
    c.course_name
from Students s
Join Enrollments e ON s.student_id = e.student_id
Join Courses c ON e.course_id = c.course_id;

/* 5. Create a query to list the names of teachers and the courses they are assigned to. Join the
"Teacher" table with the "Courses" table.*/

Select CONCAT_WS(' ',t.first_name,t.last_name) as Name , C.Course_name from Teacher t
Join Courses c on c.teacher_id=t.teacher_id Order By Name Asc;


/*6. Retrieve a list of students and their enrollment dates for a specific course. You'll need to join the
"Students" table with the "Enrollments" and "Courses" tables
 */
 Select 
    s.first_name, 
    s.last_name, 
    c.course_name,
	e.enrollment_date
from Students s
Join Enrollments e ON s.student_id = e.student_id
Join Courses c ON e.course_id = c.course_id where c.course_id=3;

/* 7. Find the names of students who have not made any payments. Use a LEFT JOIN between the
"Students" table and the "Payments" table and filter for students with NULL payment records */

Select s.Student_id ,s.first_name from Students s
Left Join Payments p On  s.student_id=p.student_id 
where
p.student_id is null;




/* 8. Write a query to identify courses that have no enrollments. You'll need to use a LEFT JOIN
between the "Courses" table and the "Enrollments" table and filter for courses with NULL
enrollment records.*/

Select c.course_id,c.course_name from Courses c
Left Join Enrollments e On c.course_id=e.course_id
where e.course_id is Null;

Select course_id from Enrollments group by course_id Order By course_id;

/* 9. Identify students who are enrolled in more than one course. Use a self-join on the "Enrollments"
table to find students with multiple enrollment records.*/

select e.student_id ,count(e.student_id) as No_of_Courses from Enrollments e
Left Join Enrollments e1  on e.student_id=e1.student_id
group by e.student_id
having count(e1.student_id)>1;

/* 10. Find teachers who are not assigned to any courses. Use a LEFT JOIN between the "Teacher"
table and the "Courses" table and filter for teachers with NULL course assignments. */

select * from Teacher t
Left Join Courses c
On t.teacher_id=c.teacher_id 
where c.course_id is Null;

-- Task - 4-----------------------------------------------------------------------------------------------------------------------

/*1. Write an SQL query to calculate the average number of students enrolled in each course. Use
aggregate functions and subqueries to achieve this*/

 SELECT course_id, Avg(student_count) AS average_students
FROM (
    SELECT course_id, COUNT(student_id) AS student_count
    FROM enrollments
    GROUP BY course_id
) AS course_enrollment_counts
GROUP BY course_id;

/*2. Identify the student(s) who made the highest payment. Use a subquery to find the maximum
payment amount and then retrieve the student(s) associated with that amount.*/

Select  Top 1 student_id, Max(Student_payment) from
  (Select Student_id , Sum(amount) as Student_payment from payments Group by Student_id) As Max_Payments Group by Student_id ;

/*3. Retrieve a list of courses with the highest number of enrollments. Use subqueries to find the
course(s) with the maximum enrollment count.*/

Select * from Enrollments

Select Enrollments_No_Max.Course_id ,c.course_name, Max(Enroll) As Enrollment  from
(Select course_id, Count(Enrollment_id) as Enroll from Enrollments Group by course_id) As Enrollments_No_Max 
Left join Courses c on Enrollments_No_Max.course_id=c.course_id 
group by Enrollments_No_Max.course_id , c.course_name
order by Enrollment desc 
;

/*4. Calculate the total payments made to courses taught by each teacher. Use subqueries to sum
payments for each teacher's courses.*/

Select  CONCAT_WS(' ',t.first_name,t.last_name) as teacher_name,
(select Sum(p.amount) from  Enrollments e
join Payments p on e.student_id=p.student_id
join Courses c on e.course_id=c.course_id
where t.teacher_id=c.teacher_id 
) As Sum_of_Amount_teacher_made from teacher t ;


/* 5. Identify students who are enrolled in all available courses. Use subqueries to compare a
student's enrollments with the total number of courses*/

Select e.student_id , CONCAT_WS(' ',s.first_name,s.last_name) as Name, Count(e.course_id) as No_of_Course from Enrollments e
join Students s on e.student_id=s.student_id
group by e.student_id ,s.first_name,s.last_name
having count(course_id)=(Select count(*) from Courses )

/*6. Retrieve the names of teachers who have not been assigned to any courses. Use subqueries to
find teachers with no course assignments.*/
select * from Teacher t 
where t.teacher_id Not In (select c.teacher_id from Courses c);

/*7. Calculate the average age of all students. Use subqueries to calculate the age of each student
based on their date of birth.
*/

Select
    AVG(age) as average_age
From (
    Select
        DATEDIFF(YEAR, date_of_birth, GETDATE())  AS age
    From 
        Students
) as student_ages;

/* 8. Identify courses with no enrollments. Use subqueries to find courses without enrollment
records */
 
 Select * from Courses c
 where c.course_id not In ( select e.course_id from Enrollments e  group by e.course_id );

/*9. Calculate the total payments made by each student for each course they are enrolled in. Use
subqueries and aggregate functions to sum payments.*/

select s.Student_id ,CONCAT_WS(' ',first_name,last_name) as [Name],c.course_name,
(Select SUM(p.amount) from Payments p where p.student_id=s.student_id) as Amt  from Students s
join Enrollments e on s.student_id=e.student_id
join Courses c on e.course_id=c.course_id

/* 10. Identify students who have made more than one payment. Use subqueries and aggregate
functions to count payments per student and filter for those with counts greater than one.*/

Select * from Students 
where student_id In (select student_id from Enrollments group by student_id having count(enrollment_id)>1);

/*11. Write an SQL query to calculate the total payments made by each student. Join the "Students"
table with the "Payments" table and use GROUP BY to calculate the sum of payments for each
student*/

select s.student_id ,s.first_name, Sum(p.amount) from Students s 
left join Payments p on s.student_id=p.student_id  group by s.student_id,s.first_name;

/* 12. Retrieve a list of course names along with the count of students enrolled in each course. Use
JOIN operations between the "Courses" table and the "Enrollments" table and GROUP BY to
count enrollments.*/

Select c.Course_id , c.Course_name , count(e.student_id) as No_of_Count  from Courses c
left join Enrollments e on c.course_id=e.course_id  group by c.Course_id , c.Course_name ;

/* 13. Calculate the average payment amount made by students. Use JOIN operations between the
"Students" table and the "Payments" table and GROUP BY to calculate the average.*/

Select s.student_id , s.first_name , avg(p.amount) as Avg_Amount from Students s
left join Payments p on s.student_id=p.student_id group by s.student_id , s.first_name ;
