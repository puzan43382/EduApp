import React from "react";

export default function Hero() {
  return (
    <>
      <section className="bg-[#EDEDED] h-56 w-full flex flex-col justify-center items-center gap-4 mb-8 ">
        <h1 className="text-3xl font-bold text-black">Start Learning Today</h1>
        <p className="text-black/80">Many courses to choose from</p>
        <input
          type="search"
          placeholder="Search Courses"
          className=" text-black/80 mt-2 px-4 py-2 rounded-md w-80 focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </section>

      <section className="m-10">
        <h2 className="font-bold mb-2">Features That we provide</h2>

        <nav className="grid grid-cols-4 gap-4 w-auto  ">
          <container className="h-36 bg-[#EDEDED] rounded-3xl">
            <article className="flex flex-col mt-6 gap-2">
              <h1 className="font-bold text-xl">Progress Tracking</h1>
              <p>
                Monitor Your Learning Journey<br></br>
                With Detailed analytics
              </p>
            </article>
          </container>
          <container className="h-36 bg-[#EDEDED] rounded-3xl">
            <article className="flex flex-col mt-6 gap-2">
              <h1 className="font-bold text-xl">Daily Quiz</h1>
              <p>
                Test Your Knowledge with Daily<br></br>
                Challenges
              </p>
            </article>
          </container>
          <container className="h-36 bg-[#EDEDED] rounded-3xl">
            <article className="flex flex-col mt-6 gap-2">
              <h1 className="font-bold text-xl">Exam Preparation</h1>
              <p>
                Get Ready To for your exams with<br></br>
                Practice tests
              </p>
            </article>
          </container>
          <container className="h-36 bg-[#EDEDED] rounded-3xl">
            <article className="flex flex-col mt-6 gap-2">
              <h1 className="font-bold text-xl">Notification</h1>
              <p>
                Stay updated with course <br></br>
                Remainders and updates
              </p>
            </article>
          </container>
        </nav>

        <h2 className="font-bold text-start m-4 ">Education Level</h2>
        <nav className="grid grid-cols-3 gap-4 text-center  ">
          <container className="h-16 bg-[#EDEDED] rounded-2xl p-5 ">
            <p className="font-bold text-xl">Secondary school</p>
          </container>
          <container className="h-16 bg-[#EDEDED] rounded-2xl p-5">
            <p className="font-bold text-xl">High School</p>
          </container>
          <container className="h-16 bg-[#EDEDED] rounded-2xl p-5">
            <p className="font-bold text-xl">Bachelors</p>
          </container>
        </nav>


<h2 className="font-bold mb-2">Education</h2>
        <section className="grid grid-cols-3 gap-4 m-10 p-6 ">
          <container
            className="h-24 bg-[#EDEDED] rounded-2xl p-5
          "
          >
            1
          </container>
          <container
            className="h-24 bg-[#EDEDED] rounded-2xl p-5
          "
          >
            3
          </container>
          <container
            className="h-24 bg-[#EDEDED] rounded-2xl p-5
          "
          >
            3
          </container>
          <container
            className="h-24 bg-[#EDEDED] rounded-2xl p-5
          "
          >
            4
          </container>
          <container
            className="h-24 bg-[#EDEDED] rounded-2xl p-5
          "
          >
            5
          </container>
          <container
            className="h-24 bg-[#EDEDED] rounded-2xl p-5
          "
          >
            6
          </container>
        </section>
      </section>
    </>
  );
}
