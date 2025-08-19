import React from 'react'

const SideBar = () => {
  return (
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
  )
}

export default SideBar