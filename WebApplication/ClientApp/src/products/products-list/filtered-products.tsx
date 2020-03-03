import React, { useEffect } from 'react';
import { createStyles, Theme, makeStyles } from '@material-ui/core/styles';
import { MenuItem, Menu } from '@material-ui/core';
import ArrowRightIcon from '@material-ui/icons/ArrowRight';
import axios from 'axios';
import { Category } from '../../categories/category';
import { async } from 'q';

export interface ICategoryList {
    categoryList: Category[]
}

interface IFilteredProductsProps {
    anchorElMenu: null | HTMLElement,
    handleCloseOpenMenu: Function
}

export default function FilteredProducts ({ anchorElMenu, handleCloseOpenMenu }: IFilteredProductsProps) {

    const useStyles = makeStyles((theme: Theme) =>
        createStyles({
            categoryItems: {
                marginLeft: theme.spacing(25)
            },
            filterButton: {
                marginTop: theme.spacing(5)
            }
        })
    );

    const classes = useStyles();
    const [anchorElSubMenu, setAnchorElSubMenu] = React.useState<null | HTMLElement>(null);
    const [categoryNames, setCategoryNames] = React.useState<ICategoryList>();

    const fetchCategories = async () => {
        const response = await axios.get(`https://localhost:44362/api/v1/category`);
      
        setCategoryNames({ ...categoryNames, categoryList: response.data });
    };

    // tslint:disable-next-line: no-floating-promises
    useEffect(() => { fetchCategories() }, []); // el segundo parametro es el then

    const handleCloseBothMenu = () => {
        handleCloseOpenMenu();
        setAnchorElSubMenu(null);
    }

    const handleClickOpenSubMenu = (event: React.MouseEvent<HTMLLIElement, MouseEvent>) => {
        setAnchorElSubMenu(event.currentTarget);
    }

    const handleCloseSubMenu = () => {
        setAnchorElSubMenu(null);
    }

    return (
        <div>
            <Menu
                id='simple-menu'
                anchorEl={anchorElMenu}
                keepMounted
                open={Boolean(anchorElMenu)}
                onClose={handleCloseBothMenu}
                className={classes.filterButton}
            >
                <MenuItem onClick={handleClickOpenSubMenu}> Filter By Category
                    <ArrowRightIcon />
                </MenuItem>
            </Menu>

            <Menu
                id='sub-menu'
                anchorEl={anchorElSubMenu}
                keepMounted
                open={Boolean(anchorElSubMenu)}
                className={classes.categoryItems}
                onClose={handleCloseSubMenu}
            >
                {categoryNames !== undefined && categoryNames.categoryList !== undefined ? categoryNames.categoryList.map(category => (
                    <MenuItem onClick={handleCloseSubMenu} key={category.name}>
                        {category.name}
                    </MenuItem>
                ))
                    : null}
            </Menu>
        </div>

    );
}
