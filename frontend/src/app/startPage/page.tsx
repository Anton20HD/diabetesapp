"use client";

import React from "react";
import PostPage from "../postPage/page";

//import Products from "@/app/components/products/page";

const StartPage = () => {
  // const [products, setProducts] = useState([]);
  // useEffect(() => {
  //   fetch("")
  //     .then((res) => {
  //       return res.json();
  //     })
  //     .then((data) => {
  //       console.log(data);
  //       setProducts(data);
  //     });
  // }, []);

  return (
    <div className="h-screen bg-repeat bg-cover bg-center absolute overflow-hidden">
      <div className="fixed">
        <div className="fixed top-0 left-0 h-screen w-48 z-1 pt-30 bg-white">
          <ul className=" cursor-pointer  text-sm text-gray-400 flex leading-15 flex-col justify-center items-center  no-underline">
            <li>Home</li>
            <li>Popular</li>
            <li>Explore</li>
            <li>All</li>
          </ul>

           <ul className=" cursor-pointer  text-gray-400 flex leading-15 flex-col justify-center items-center text-sm  no-underline">
          <h3 className="text-blue-950 font-bold">Communities</h3>
            <li>Create Community</li>
            <li>Manage communities</li>
          </ul>

           <ul className=" cursor-pointer  text-gray-400 flex leading-15 flex-col justify-center items-center text-sm   no-underline">
          <h3 className="text-blue-950 font-bold">Resources</h3>
            <li>About us</li>
            <li>Contact us</li>
          </ul>
          
        </div>

        <video
          className="w-600 h-150  object-cover"
          src="/videos/startpage.mp4"
          autoPlay
          loop
          muted
        />

        <div className="absolute top-13/25 left-13/25 bg-transparent transform -translate-x-1/2 -translate-y-1/2 py-[10px] px-[20px] flex justify-center flex-col tracking-widest">
          <h1 className="text-white text-2xl  mb-5 font-bold">
            Join the diabetes community today — start sharing your story
          </h1>
          <h4 className="text-[#E0F7FA] font-semibold text-shadow">
            — Got questions about life with diabetes? Whether it’s about sports,
            relationships, or personal challenges, we’ve got you covered.
            Explore different categories to find communities that match your
            interests, and connect with people who truly understand —
          </h4>
        </div>
        <PostPage/>
      </div>

    </div>
  );
};

export default StartPage;
