import React from "react";

export default function NavBar() {
  return (
    <>
      <div className="flex justify-between mb-1 mt-5 px-4">
        <h2 className="py-2 font-bold">EduApp</h2>
        <div className="flex gap-2">
          <button className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600">
            Log in
          </button>
          <button className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600">
            Sign Up
          </button>
        </div>
      </div>

   

    </>
  );
}
