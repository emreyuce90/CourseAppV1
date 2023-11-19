import { Button, Popup } from "devextreme-react";
import { RequiredRule } from "devextreme-react/data-grid";
import Form, { ButtonItem, SimpleItem } from "devextreme-react/form";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { addCourse, getCourses } from "../course/courseSlice";
import { DisabledByDefaultOutlined } from "@mui/icons-material";

const AddCourseComponent = () => {
    const dispatch = useDispatch();
  const [form, setForm] = useState({
    title: "",
    description: "",
  });
  const [isOpen, setIsOpen] = useState(false);
  const handleClick = () => {
    setIsOpen(true);
  };
  const hideInfo = () => {
    setIsOpen(false);
  };
  const submitButtonOptions = {
    width: "100%",
    text: "Add Course",
    type: "default",
    useSubmitBehavior: true,
    stylingMode: "contained", // Stil modu: 'outlined', 'contained', 'text'
    elementAttr: {
      class: "rounded-full p-4 mt-10 text-lg !bg-[#4f46e5]",
    },
  };
  const validationRules = {
    title: [{ type: "required", message: "FullName is required" }],
    description: [{ type: "required", message: "Description is required" }],
  };
  const formHandleSubmit =async (e) => {
    e.preventDefault();
    await dispatch(addCourse(form))
    setIsOpen(false);
dispatch(getCourses())
  };
  return (
    <>
      <Button type="success" text="Add Course" onClick={handleClick} />
      <Popup
        visible={isOpen}
        showTitle={true}
        title="Add Sample Course"
        container=".dx-viewport"
        width={300}
        height={280}
        onHiding={hideInfo}
      >
        <form onSubmit={formHandleSubmit}>
          <Form formData={form} labelMode="static">
            <SimpleItem dataField="title" />
            <RequiredRule message={validationRules.title}/>
            <SimpleItem dataField="description" />
            <RequiredRule message={validationRules.description}/>

            <ButtonItem buttonOptions={submitButtonOptions} />
          </Form>
        </form>
      </Popup>
    </>
  );
};

export default AddCourseComponent;
