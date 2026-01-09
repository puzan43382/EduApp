import React from "react";

export default function Hero() {
  return (
    <>
      <section className="bg-[#767676] h-56 w-full flex flex-col justify-center items-center gap-4 mb-8 ">
        <h1 className="text-3xl font-bold text-white">Start Learning Today</h1>
        <p className="text-white/80">Many courses to choose from</p>
        <input
          type="search"
          placeholder="Search Courses"
          className="mt-2 px-4 py-2 rounded-md w-80 focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </section>

      <section>
        <h2>Features That we provide</h2>

        <nav className="grid grid-cols-4 gap-4 w-full">
          <container className="h-36 bg-[#767676] rounder-3xl">1</container>
          <container className="h-36 bg-[#767676] rounder-2xl">2</container>
          <container className="h-36 bg-[#767676] rounder-2xl">3</container>
          <container className="h-36 bg-[#767676] rounder-2xl">4</container>
        </nav>
      </section>
    </>
  );
}
