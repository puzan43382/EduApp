import { fetchData } from "./api.js";
const SchoolURL = "https://localhost:7089/api/AcademicProgram/GetProgramWithCourse?Id=3"
const SchoolBtn = document.getElementById("SchoolBtn");
const SchoolContainer = document.getElementById("Schooldiv");
SchoolContainer.style.display = "none";

function renderSchool(Schools) {
  SchoolContainer.innerHTML = '';
  if (!Schools || Schools.length === 0) {
    SchoolContainer.textContent = "No courses available.";
    return;
  }
  if (!Array.isArray(Schools) || Schools.length === 0) {
    SchoolContainer.textContent = "No program available.";
    return;
  }
  const container = document.createElement("div");
  container.style.display = "flex";
container.style.flexWrap = "wrap";
container.style.gap = "10px";
  Schools.forEach(School => {
    School.courses.forEach(course => {
      const box = document.createElement("div");
      box.textContent = course.courseName;
       box.style.border = "1px solid black";
    box.style.padding = "10px";
    box.style.borderRadius = "5px";
    box.style.minWidth = "120px";
    box.style.textAlign = "center";
      container.appendChild(box);
    });
  });
  SchoolContainer.appendChild(container);
}



async function loadSchool() {
  try {
    const data = await fetchData(SchoolURL);
    console.log("API response:", data);
    SchoolContainer.style.display = "block";
    renderSchool(data);
  } catch (error) {
    console.error("Error loading school data:", error);
  }
}
SchoolBtn.addEventListener("click", loadSchool);
