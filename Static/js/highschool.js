import { fetchData } from "./api.js";

const HighSchoolURL = "https://localhost:7089/api/AcademicProgram/GetProgramWithCourse?Id=2"
const HighSchoolBtn = document.getElementById("HighSchoolBtn");
const HighSchoolContainer = document.getElementById("HighSchooldiv");
HighSchoolContainer.style.display = "none";

function renderSchool(Schools) {
    HighSchoolContainer.innerHTML = '';
    if (!Schools || Schools.length === 0) {
        HighSchoolContainer.textContent = "No courses available.";
        return;
    }
    if (!Array.isArray(Schools) || Schools.length === 0) {
        HighSchoolContainer.textContent = "No program available.";
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
    HighSchoolContainer.appendChild(container);
}



async function loadHighSchool() {
    try {
        const data = await fetchData(HighSchoolURL);
        console.log("API response:", data);
        HighSchoolContainer.style.display = "block";
        renderSchool(data);
    } catch (error) {
        console.error("Error loading high school data:", error);
    }
}
HighSchoolBtn.addEventListener("click", loadHighSchool);
