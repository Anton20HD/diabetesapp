"use client";

import React from "react";
import PostPage from "../postPage/page";
import SideBar from "../components/SideBar/SideBar";

const StartPage = () => {
  return (
    <div className="h-screen bg-repeat bg-cover bg-center">
      <div className="fixed">
        <SideBar />
      </div>
      <div className="flex-1 ml-60 overflow-y-auto">
        <div className="sticky top-0">
          <video
            className="w-600 h-150 object-cover"
            src="/videos/startpage.mp4"
            autoPlay
            loop
            muted
          />

          <div
            className="absolute inset-0 flex flex-col items-center justify-center px-4 text-center tracking-widest
"
          >
            <h1 className="text-white text-2xl  mb-5 font-bold">
              Join the diabetes community today — start sharing your story
            </h1>
            <h4 className="text-[#E0F7FA] font-semibold text-shadow">
              — Got questions about life with diabetes? Whether it’s about
              sports, relationships, or personal challenges, we’ve got you
              covered. Explore different categories to find communities that
              match your interests, and connect with people who truly understand
              —
            </h4>
          </div>
        </div>
        <PostPage />
      </div>
    </div>
  );
};

export default StartPage;

//absolute top-13/25 left-13/25 bg-transparent transform -translate-x-1/2 -translate-y-1/2 py-[10px] px-[20px] flex justify-center flex-col tracking-widest
