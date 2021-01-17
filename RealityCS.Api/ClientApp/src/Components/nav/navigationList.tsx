const navigation = [
    {
        Name: "Dashboard",
        Child: [
            {
                Name: "Home",
                class: "fas fa-home",
                Link: "/dashboard"
            }
        ]
    },
    {
        Name: "Data Import",
        Child: [
            {
                Name: "Home",
                class: "fas fa-home",
                Link: "/dashboard"
            },
            {
                Name: "Data Import",
                class: "fas fa-file-import",
                Link: "/import"
            },
            {
                Name: "Data Mapper",
                class: "fas fa-file-import",
                Link: "/data-mapper"
            }
        ]
    },
    {
        Name: "User Access Management",
        Child: [
            {
                Name: "Home",
                class: "fas fa-home",
                Link: "/dashboard"
            }, {
                Name: "Users",
                class: "fas fa-users",
                Link: "/usermanagement"
            },
            {
                Name: "Access Group",
                class: "fas fa-file-import",
                Link: "/accessgroup"
            },
            {
                Name: "Access Operations",
                class: "fa fa-database",
                Link: "/accessoperations"
            }
        ]
    },
    {
        Name:"KPI Elements",
        Child:[
            {
                Name: "Home",
                class: "flaticon-home",
                Link: "/dashboard"
            }, {
                Name: "KPI Elements",
                class: "flaticon-information",
                Link: "/KPI-Information"
            },
            {
                Name: "KPI Benchmark",
                class: "flaticon-performance",
                Link: "/accessgroup"
            },
            {
                Name: "KPI Threshold",
                class: "flaticon-dashboard-1",
                Link: "/accessoperations"
            }   
        ]
    }
    // ,{
    //     Name: "Customer Onboarding",
    //     Child: [
    //         {
    //             Name: "Home",
    //             class: "fas fa-home",
    //             Link: "/dashboard"
    //         }, {
    //             Name: "Register Legal Entity",
    //             class: "fab fa-wpforms",
    //             Link: "/import"
    //         },
    //         {
    //             Name: "Update Legal Entity",
    //             class: "far fa-file",
    //             Link: "/import"
    //         }
    //     ]
    // }
]
export default navigation;
