import * as React from "react";
import { styled, alpha } from "@mui/material/styles";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import InputBase from "@mui/material/InputBase";
import Badge from "@mui/material/Badge";
import MenuItem from "@mui/material/MenuItem";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import SearchIcon from "@mui/icons-material/Search";
import AccountCircle from "@mui/icons-material/AccountCircle";
import MailIcon from "@mui/icons-material/Mail";
import NotificationsIcon from "@mui/icons-material/Notifications";
import MoreIcon from "@mui/icons-material/MoreVert";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import { User } from "../../types/User";
import SignUp from "../Services/SignUp";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import useAppDispatch from "../../hooks/useAppDispatch";
import useAppSelector from "../../hooks/useAppSelector";
import { userLogout } from "../../redux/reducers/authenticationReducer";
import { useDebounce } from "use-debounce";
import { FetchQuery, SearchBooksByTitle, fetchAllBooks } from "../../redux/reducers/booksReducer";

const Search = styled("div")(({ theme }) => ({
  position: "relative",
  borderRadius: theme.shape.borderRadius,
  backgroundColor: alpha(theme.palette.common.white, 0.15),
  "&:hover": {
    backgroundColor: alpha(theme.palette.common.white, 0.25),
  },
  marginRight: theme.spacing(2),
  marginLeft: 0,
  width: "100%",
  [theme.breakpoints.up("sm")]: {
    marginLeft: theme.spacing(3),
    width: "auto",
  },
}));

const SearchIconWrapper = styled("div")(({ theme }) => ({
  padding: theme.spacing(0, 2),
  height: "100%",
  position: "absolute",
  pointerEvents: "none",
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
}));

const StyledInputBase = styled(InputBase)(({ theme }) => ({
  color: "inherit",
  "& .MuiInputBase-input": {
    padding: theme.spacing(1, 1, 1, 0),
    // vertical padding + font size from searchIcon
    paddingLeft: `calc(1em + ${theme.spacing(4)})`,
    transition: theme.transitions.create("width"),
    width: "100%",
    [theme.breakpoints.up("md")]: {
      width: "20ch",
    },
  },
}));

const Header : React.FC<FetchQuery> = ({offset, limit}) => {
  const dispatch = useAppDispatch();
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [mobileMoreAnchorEl, setMobileMoreAnchorEl] =
    React.useState<null | HTMLElement>(null);
  const isMenuOpen = Boolean(anchorEl);
  const isMobileMenuOpen = Boolean(mobileMoreAnchorEl);
  const [showCartPage, setShowCartPage] = useState(false);
  const [showSignUp, setShowSignUp] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [userProfile, setUserProfile] = useState<User | null>(null);
  const [cartItemCount, setCartItemCount] = useState(0);
  const [searchTerm, setSearchTerm] = useState('');
  const [debouncedSearchTerm] = useDebounce(searchTerm, 300);
  
  const handleSignUpClick = () => {
    setShowSignUp(!showSignUp);
  };

  const handleCartPageClose = () => {
    setShowCartPage(false);
  };

  const handleProfileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMobileMenuClose = () => {
    setMobileMoreAnchorEl(null);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    handleMobileMenuClose();
  };

  const handleMobileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setMobileMoreAnchorEl(event.currentTarget);
  };

  const storedUserProfile = localStorage.getItem("userProfile");

  const handleLogout = () => {
    dispatch(userLogout());
    setIsLoggedIn(false);
  };

  useEffect(() => {
    if (storedUserProfile) {
      const parsedUserProfile = JSON.parse(storedUserProfile);
      setUserProfile(parsedUserProfile);
    } else {
      setUserProfile(null);
    }

    const cartItems = localStorage.getItem("cartItems");
    if (cartItems) {
      const parsedCartItems = JSON.parse(cartItems);
      setCartItemCount(parsedCartItems.length);
    } else {
      setCartItemCount(0);
    }
  }, [storedUserProfile, localStorage.getItem("cartItems")]);

  const menuId = "primary-search-account-menu";

  const renderMenu = (
    <Menu
      anchorEl={anchorEl}
      anchorOrigin={{
        vertical: "top",
        horizontal: "right",
      }}
      id={menuId}
      keepMounted
      transformOrigin={{
        vertical: "top",
        horizontal: "right",
      }}
      open={isMenuOpen}
      onClose={handleMenuClose}
    >
      {!userProfile ? (
        <>
          <MenuItem>
            <Link
              to="/signup"
              style={{ textDecoration: "none", color: "inherit" }}
            >
              Sign Up
            </Link>
          </MenuItem>
          <MenuItem>
            <Link
              to="/login"
              style={{ textDecoration: "none", color: "inherit" }}
            >
              Login
            </Link>
          </MenuItem>
        </>
      ) : (
        <>
          {userProfile.role === "admin" && (
            <MenuItem>
              <Link
                to="/dashboard"
                style={{ textDecoration: "none", color: "inherit" }}
              >
                Dashboard
              </Link>
            </MenuItem>
          )}
          <MenuItem>
            <Link
              to="/profile"
              style={{ textDecoration: "none", color: "inherit" }}
            >
              Profile
            </Link>
          </MenuItem>
          <MenuItem>
            <Link
              to="/"
              onClick={handleLogout}
              style={{ textDecoration: "none", color: "inherit" }}
            >
              Log Out
            </Link>
          </MenuItem>
        </>
      )}
    </Menu>
  );

  {
    showSignUp && <SignUp />;
  }

  const mobileMenuId = "primary-search-account-menu-mobile";

  useEffect(() => {
    if (debouncedSearchTerm === "") {
      dispatch(fetchAllBooks({offset, limit}));
    } 
    else { 
    dispatch(SearchBooksByTitle({ search: debouncedSearchTerm }));
     }
    console.log("DebouncedSearchTerm: ", debouncedSearchTerm)
    console.log("searchTerm: ", searchTerm)
  }, [debouncedSearchTerm]);
  
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="fixed">
        <Toolbar>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            aria-label="open drawer"
            sx={{ mr: 2 }}
          >
            <MenuIcon />
          </IconButton>
          <Typography
            variant="h6"
            noWrap
            component="div"
            sx={{ display: { xs: "none", sm: "block" } }}
          >
            <Link to="/" style={{ textDecoration: "none", color: "white" }}>
              HOME
            </Link>
          </Typography>
          <Search>
            <SearchIconWrapper>
              <SearchIcon />
            </SearchIconWrapper>
            <StyledInputBase
              placeholder="Search by title…"
              inputProps={{ "aria-label": "search" }}
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </Search>

          <Box sx={{ flexGrow: 1 }} />
          {userProfile && (
            <div>
              <h2>Welcome, {userProfile?.name}!</h2>
            </div>
          )}
          <Box sx={{ display: { xs: "none", md: "flex" } }}>
            <Link
              to="/cart"
              style={{ textDecoration: "none", color: "inherit" }}
            >
              <IconButton
                size="large"
                aria-label="shopping cart"
                color="inherit"
              >
                {cartItemCount > 0 && (
                  <Badge badgeContent={cartItemCount} color="error" />
                )}
                <ShoppingCartIcon />
              </IconButton>
            </Link>

            <IconButton
              size="large"
              edge="end"
              aria-label="account of current user"
              aria-controls={menuId}
              aria-haspopup="true"
              onClick={handleProfileMenuOpen}
              color="inherit"
            >
              <AccountCircle />
            </IconButton>
          </Box>
          <Box sx={{ display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-label="show more"
              aria-controls={mobileMenuId}
              aria-haspopup="true"
              onClick={handleMobileMenuOpen}
              color="inherit"
            >
              <MoreIcon />
            </IconButton>
          </Box>
        </Toolbar>
      </AppBar>
      {/* {renderMobileMenu} */}
      {renderMenu}
      {showSignUp && <SignUp />}
    </Box>
  );
};

export default Header;
