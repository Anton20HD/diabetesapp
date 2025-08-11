"use client";

import React  from "react";

import Paper from "@mui/material/Paper";
import InputBase from "@mui/material/InputBase";
import IconButton from "@mui/material/IconButton";
import SearchIcon from "@mui/icons-material/Search";
import SearchDropdown from "../SearchDropdown/SearchDropdown";

interface SearchBarProps {
  isDropdownVisible: boolean;
  toggleDropdown: () => void;
}

const SearchBar: React.FC<SearchBarProps> = ({ isDropdownVisible, toggleDropdown }) => {





  return (
    <>
    {isDropdownVisible && (
      <div className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-60 z-1 opacity-0 animate-fadeIn" onClick={toggleDropdown}></div>
    )}


    <div className="relative overflow-hidden flex justify-between flex-row items-center bg-white">
      <Paper
        component="form"
        onClick={(e) => {

          e.stopPropagation();
          toggleDropdown(); 
        }}
        sx={{
          p: "2px 4px",
          display: "flex",
          alignItems: "center",
          width: 550,
          backgroundColor: "#f4f4f4",
          boxShadow: "none",
          borderRadius: "20px",
        }}
      >
        <IconButton sx={{ p: "10px" }} aria-label="menu">
          <SearchIcon />
        </IconButton>
        <InputBase
           className="items-center"
          sx={{ ml: 1, flex: 1 }}
          placeholder="Search on SugarMates.."
          inputProps={{ "aria-label": "search clothes" }}
        />
      </Paper>
      {isDropdownVisible && <SearchDropdown toggleDropdown={toggleDropdown} isVisible={isDropdownVisible} />}
    </div>
    </>
  );
};

export default SearchBar;
