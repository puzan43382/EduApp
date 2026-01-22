import { fetchData } from "./api.js";

const BachelorURL = "https://localhost:7089/api/AcademicProgram/GetProgramWithCourse?Id=3"
const BachelorBtn = document.getElementById("BachelorBtn");
const BachelorContainer = document.getElementById("Bachelordiv");
BachelorContainer.style.display = "none";

function renderSchool(Schools) {
  BachelorContainer.innerHTML = '';
  if (!Schools || Schools.length === 0) {
    BachelorContainer.textContent = "No courses available.";
    return;
  }
  if (!Array.isArray(Schools) || Schools.length === 0) {
    BachelorContainer.textContent = "No program available.";
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
  BachelorContainer.appendChild(container);
}



async function loadBachelor() {
  try {
    const data = await fetchData(BachelorURL);
    console.log("API response:", data);
    BachelorContainer.style.display = "block";
    renderSchool(data);
  } catch (error) {
    console.error("Error loading bachelor data:", error);
  }
}
BachelorBtn.addEventListener("click", loadBachelor);