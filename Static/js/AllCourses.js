import { fetchData } from "./api.js";

const AllCourseURL = "https://localhost:7089/api/Course/AllCourse"
const AllCourseBtn = document.getElementById("AllCourseBtn");
const CourseContainer = document.getElementById("programs");
CourseContainer.style.display = "none";

function renderCourses(courses) {
  CourseContainer.innerHTML = '';
  if (!courses || courses.length === 0) {
    CourseContainer.textContent = "No courses available.";
    return;
  }

  const container = document.createElement("div");
  container.style.display = "flex";
  container.style.flexWrap = "wrap";
  container.style.gap = "10px";
  courses.forEach(course => {
    const box = document.createElement("div");
    box.innerHTML = `Course Name: ${course.title || "No Title"}<br>
    Course Code: (${course.courseCode || "No Code"})`;
    box.style.border = "1px solid black";
    box.style.padding = "10px";
    box.style.borderRadius = "5px";
    box.style.minWidth = "120px";
    box.style.textAlign = "center";
    container.appendChild(box);
  });
  CourseContainer.appendChild(container);
}


async function loadAllCourse() {
  const data = await fetchData(AllCourseURL);
  CourseContainer.style.display = "block";
  renderCourses(data);
}
loadAllCourse();
//AllCourseBtn.addEventListener("click", loadAllCourse);




// GetAllCoursesByProgram
const GetAllCoursesByProgramURL = "https://localhost:7089/api/ProgramCourse/GetAllCoursesByProgramId?Programid=0&page=1&pageSize=2";
let GetAllCoursesByProgramBtn = document.getElementById("GetAllCoursesByProgramBtn");

const CoursesByProgramsContainer = document.getElementById("CoursesByPrograms");
function renderCoursesByPrograms(CoursesByProgram) {
  CoursesByProgramsContainer.innerHTML = '';
  if (!CoursesByProgram || CoursesByProgram.length === 0) {
    CoursesByProgramsContainer.textContent = "No courses available for the selected program.";
    return;
  }
  const ul = document.createElement("ul");
  CoursesByProgram.forEach(CourseByProgram => {
    const lis = document.createElement("li");
    lis.innerHTML = `CourseId: ${CourseByProgram.programID || "No ID"}
    <br/>Course Name: ${CourseByProgram.programName || "No Title"}`;
    ul.appendChild(lis);
  });
  CoursesByProgramsContainer.appendChild(ul);
}

async function loadGetAllCoursesByProgram() {
  const datas = await fetchData(GetAllCoursesByProgramURL);
  renderCoursesByPrograms(datas);
}

GetAllCoursesByProgramBtn.addEventListener("click", loadGetAllCoursesByProgram);












// function Myfunction() {
//   console.log("Hello, World!");
// }
// Myfunction();

// let headings = document.getElementsByClassName("heading");
// console.log(headings);
// console.dir(headings);

// let newbtn = document.createElement("button");
// newbtn.innerText = "Click Me!";
// console.log(newbtn);

// // let heading = document.getElementsClassName("div")[0];
// // div1.appendChild(newbtn);
// let div1 = document.querySelector("h1");
// div1.appendChild(newbtn);






// let data = "secret information";

// class user {
//   constructor(name, email) {
//     this.name = name;
//     this.email = email;
//   }

//   viewdata() {
//     console.log("data= ", data);
//   }
// }
// let user1 = new user("Alice", "alice@example.com");
// //console.log(user1);
// console.log(user1.viewdata());
// let user2 = new user("Bob", "bob@gmail.com");
// //console.log(user2);
// console.log(user2.viewdata());