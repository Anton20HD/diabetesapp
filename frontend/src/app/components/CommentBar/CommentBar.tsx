"use client";

import React from "react";

import Paper from "@mui/material/Paper";
import InputBase from "@mui/material/InputBase";
import IconButton from "@mui/material/IconButton";
import SearchIcon from "@mui/icons-material/Search";
import SearchDropdown from "../SearchDropdown/SearchDropdown";
import Button from "@mui/material/Button";
import { SendIcon } from "lucide-react";

const CommentBar = ({}) => {
  return (
    <>
      <div className="relative overflow-hidden flex justify-between flex-row items-center bg-white">
        <Paper
          component="form"
          onClick={(e) => {
            e.stopPropagation();
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
          <InputBase
            className="items-center"
            sx={{ ml: 1, flex: 1 }}
            placeholder="Send a comment"
            inputProps={{ "aria-label": "comment" }}
          ></InputBase>
          <Button
            className="bg-black"
            variant="contained"
            endIcon={<SendIcon />}
            sx={{
              backgroundColor: "#1E3A8A",
            }}
          >
            Comment
          </Button>
        </Paper>
      </div>
    </>
  );
};

export default CommentBar;
