import { useState } from "react";

import "./App.css";
import Homepage from "./components/Homepage/homepage";
import CourseList from "./components/course-page/course-list";

function App() {
  const [count, setCount] = useState(0);

  return (
    <>
      <div>
        <Homepage></Homepage>
        <CourseList />
      </div>
    </>
  );
}

export default App;
