import { useState } from "react";

import "./App.css";
import NavBar from "./components/navigation";

function App() {
  const [count, setCount] = useState(0);

  return (
    <>
      <div>
        <NavBar></NavBar>
      </div>
    </>
  );
}

export default App;
