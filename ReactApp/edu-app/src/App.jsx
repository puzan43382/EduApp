import { useState } from "react";

import "./App.css";
import Homepage from "./components/Homepage/homepage";

function App() {
  const [count, setCount] = useState(0);

  return (
    <>
      <div >
        <Homepage></Homepage>
      </div>
    </>
  );
}

export default App;
