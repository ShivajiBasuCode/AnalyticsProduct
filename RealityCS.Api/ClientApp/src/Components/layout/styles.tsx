import { makeStyles, useTheme } from "@material-ui/core/styles";
const drawerWidth = 240;
const useStyles = makeStyles(theme => ({
    root: {
        // display: "flex"
    },
    appBar: {
        boxShadow: "none",
        backgroundColor: "#fff",
        transition: theme.transitions.create(["margin", "width"], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen
        })
    },
    appBarShift: {
        width: `calc(100% - ${drawerWidth}px)`,
        marginLeft: drawerWidth,
        transition: theme.transitions.create(["margin", "width"], {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen
        })
    },
    menuButton: {
        marginRight: '.5rem'
    },
    hide: {
        display: "none"
    },
    drawer: {
        width: drawerWidth,
        flexShrink: 0
    },
    drawerPaper: {
        width: drawerWidth
    },
    drawerHeader: {
        display: "flex",
        alignItems: "center",
        padding: theme.spacing(0, 1),
        ...theme.mixins.toolbar,
        justifyContent: "flex-end"
    },
    content: {
        flexGrow: 1,
        // padding: theme.spacing(3),
        transition: theme.transitions.create("margin", {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen
        }),
        // marginLeft: -drawerWidth
    },
    contentShift: {
        transition: theme.transitions.create("margin", {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen
        }),
        marginLeft: 0
    },
    menuWrapper: {
        marginTop: '10PX'
    },
    menucontainer: {
        borderBottom: '1px solid #d0d0d0'
    },
    leftMenu: {
        display: 'inline-flex',
        float: 'left',
        width: '19%',
        padding: '0px',
        boxSizing: 'border-box',
        paddingLeft: '26px',
        paddingTop: '44px'
    },
    rightMenu: {
        display: 'block',
        float: 'left',
        width: '81%'
    },
    righimgWrapper: {
        width: '100%',
        "&::after": {
            content: '\'\'',
            display: 'table',
            clear: 'both'
        }
    },
    menuRightWrapper: {
        width: '100%',
        marginLeft: '67px',
        "&::after": {
            content: '\'\'',
            display: 'table',
            clear: 'both'
        }
    },
    btnRoot: {
        padding: '8px 23px 9px 23px',
        fontSize: '1rem',
        fontWeight: 'bold',
        fontFamily: 'Roboto',
        color: '#0C3353',
        textTransform: 'capitalize',
        lineHeight: '1.8rem',
        borderRadius: 0,
        letterSpacing: '0px',
        '&:hover': {
            backgroundColor: "#E24500",
            color: '#fff',
        }
    },
    activeRoot: {
        backgroundColor: "#EE6123",
        color: "#fff",
        fontWeight: 400,
        '&:hover': {
            backgroundColor: "#E24500",
        }
    },
    publicHeader: {
        backgroundColor: '#fff',
        boxShadow: 'none',
    },
    menucontainerpublic: {
        borderBottom: '1px solid rgba(130, 130, 130, .3)',
        boxShadow: 'none'
    },
    alignAppToolbarFirstImg: {
        float: "right"
    },
    alignAppToolbarSecondImg: {
        float: "right",
        height: '83px'
    },
    alignCustomLink: {
        backgroundColor: "transparent",
        color: "#fff",
        textDecoration: "none",
        display: 'inline-block'
    },
    alignHeaderLink: {
        padding: "0.8rem 1.8rem",
        color: "#0C3353",
        textDecoration: "none",
        display: 'inline-block',
        fontSize: '1rem',
        fontWeight: 500
    },
    mcHeaderItemColor: {
        background: "#F2F2F2"
    },
    alignDrawerImg: {
        maxWidth: "70%",
        width: "100%",
        padding: "1rem"
    },
    alignDialogContent: {
        marginTop: "2rem",
        padding: "0 2rem"
    },
    alignDialogTitle: {
        paddingTop: "1.5rem",
        paddingBottom: "1.5rem",
        borderBottom: "1px solid #BDBDBD"
    },
    alignTCTypography: {
        color: "#4F4F4F",
        fontSize: "18px",
        fontWeight: 600
    },
    alignDialogActions: {
        borderTop: "1px solid #BDBDBD"
    },
    menuBtn: {
        color: '#0c3353',
        padding: '14px 30px 14px 30px',
        fontSize: '1rem',
        fontFamily: 'Roboto',
        fontWeight: 500,
        lineHeight: '1.1875rem',
        borderRadius: 0,
        letterSpacing: '0px',
        textTransform: 'capitalize',
        textDecoration: 'none',
        display: 'inline-block',
        outline: 'none',
        minWidth: 'initial !important',
        verticalAlign: 'top',
        '&:hover': {
            backgroundColor: '#E24500',
            color: '#fff',
        }
    },
    iconButtonColor: {
        color: "#EE6123"
    },
    alignPrivacyStatement: {
        marginBottom: "1rem"
    }
}));
export { useStyles };
