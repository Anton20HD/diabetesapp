"use client";

import React, { useEffect, useState } from "react";
import Link from "next/link";
import SearchIcon from "@mui/icons-material/Search";
import HomeIcon from "@/app/assets/sugar-mates.png"
import { Button } from "@/components/ui/button"

import { useRouter } from "next/navigation";
import SearchBar from "../SearchBar/SearchBar";

const Header = () => {
  const router = useRouter();

  const [isMobile, setIsMobile] = useState(false);
  const [isVisible, setIsVisible] = useState(false);
  const [isDropdownVisible, setIsDropdownVisible] = useState(false);

  const toggleMenu = () => {
    setIsVisible((prev) => !prev);
  };

  const toggleDropdown = () => {
    setIsDropdownVisible((prevState) => !prevState);
  };

    const handleProfileClick = () => {
    
  };

  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= 768);
    };

    handleResize();
    window.addEventListener("resize", handleResize);
    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  //   const handleProfileClick = () => {
  //     if (session) {
  //       router.push("/dashboard"); //Navigate to dashboard if logged in
  //     } else {
  //       router.push("/login"); // Navigate to login if not logged in
  //     }
  //   };

  return (
    <>
      {isVisible && <div className=" fixed top-0 left-0 w-full h-full  bg-black bg-opacity-60 z-1 opacity-0 animate-fadeIn" onClick={toggleMenu}></div>}

      


      <div className=" fixed top-0 left-0 w-full z-50 bg-white flex justify-between items-center h-25">
        <div className="z-2">
          <Link href="/" passHref>
            <div>
              <img className="w-55 h-40 ml-0 cursor-pointer" src={HomeIcon.src} alt="Home" />
            </div>
          </Link>
        </div>

  
        <div className="flex items-center ml-0 ">
          {!isMobile ? (
            <SearchBar
              isDropdownVisible={isDropdownVisible}
              toggleDropdown={toggleDropdown}
            />
          ) : (
            <div
              className="cursor-pointer text-[24px] bg-black "
              onClick={() => {
                setIsDropdownVisible(true);
              }}
            >
              <SearchIcon />
            </div>
          )}
        </div>

        
  

        {isMobile && isDropdownVisible && (
          <div>
            <SearchBar
              isDropdownVisible={isDropdownVisible}
              toggleDropdown={toggleDropdown}
            />
          </div>
        )}


        <div className="flex -translate-x-1/2 gap-5   mr-25">
          <Button className="flex cursor-pointer bg-blue-950 w-20 h-12 items-center justify-center rounded-[30]" onClick={handleProfileClick}>
            <h2 className="text-center">Log in</h2>
          </Button>
        </div>
      </div>
    </>
  );
};

export default Header;
