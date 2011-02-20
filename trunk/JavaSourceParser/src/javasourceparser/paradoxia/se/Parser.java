package javasourceparser.paradoxia.se;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;

import org.eclipse.core.resources.IProject;
import org.eclipse.core.resources.IWorkspace;
import org.eclipse.core.resources.IWorkspaceRoot;
import org.eclipse.core.resources.ResourcesPlugin;
import org.eclipse.core.runtime.Plugin;
import org.eclipse.jdt.core.IJavaProject;
import org.eclipse.jdt.core.JavaCore;
import org.eclipse.jdt.core.dom.AST;
import org.eclipse.jdt.core.dom.ASTNode;
import org.eclipse.jdt.core.dom.ASTParser;
import org.eclipse.jdt.core.dom.ASTVisitor;
import org.eclipse.jdt.core.dom.CompilationUnit;
import org.eclipse.jdt.core.dom.Name;
import org.eclipse.jdt.core.dom.SimpleName;
import org.eclipse.jdt.core.dom.VariableDeclarationFragment;
import org.eclipse.jdt.internal.core.JavaProject;
import org.osgi.framework.BundleContext;

public class Parser {

	/**
	 * @param args
	 */

	static public String getContents(File aFile) {
		// ...checks on aFile are elided
		StringBuilder contents = new StringBuilder();

		try {
			// use buffering, reading one line at a time
			// FileReader always assumes default encoding is OK!
			BufferedReader input = new BufferedReader(new FileReader(aFile));
			try {
				String line = null; // not declared within while loop
				/*
				 * readLine is a bit quirky : it returns the content of a line
				 * MINUS the newline. it returns null only for the END of the
				 * stream. it returns an empty String if two newlines appear in
				 * a row.
				 */
				while ((line = input.readLine()) != null) {
					contents.append(line);
					contents.append(System.getProperty("line.separator"));
				}
			} finally {
				input.close();
			}
		} catch (IOException ex) {
			ex.printStackTrace();
		}

		return contents.toString();
	}
	
	static public void WriteToTextFile(String Text) 
	{
		try {
			
			FileWriter fw = new FileWriter(outputFileName,true);
			
			BufferedWriter writer = new BufferedWriter(fw);
			writer.write(Text+"\r\n");
			writer.close();
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		
	}

	public static String outputFileName = "";
	
	public static void main(String[] args) {

		System.out.println("InputFile:" + args[0]);

		File file = new File(args[0]);
		String justFileName = file.getName();
		int index = justFileName.lastIndexOf('.');
		outputFileName = args[1] + "\\" + justFileName.substring(0, index)
				+ ".jsp";

		String s = getContents(file);

		// System.out.println(s);

		ASTParser parser = ASTParser.newParser(AST.JLS3);

		parser.setResolveBindings(true);
		parser.setStatementsRecovery(true);
		parser.setBindingsRecovery(true);
		parser.setSource(s.toCharArray());

		parser.setKind(ASTParser.K_COMPILATION_UNIT);
		// ASTNode node = parser.createAST(null);

		final CompilationUnit cu = (CompilationUnit) parser.createAST(null);

		cu.accept(new ASTVisitor() {

			Set names = new HashSet();

			public boolean visit(org.eclipse.jdt.core.dom.MethodDeclaration node) {
				SimpleName name = node.getName();
				WriteToTextFile("MethodDeclaration of '" + name
						+ "' at line"
						+ cu.getLineNumber(name.getStartPosition()));
//				System.out.println("MethodDeclaration of '" + name
//						+ "' at line"
//						+ cu.getLineNumber(name.getStartPosition()));

				return true;
			}

			public boolean visit(
					org.eclipse.jdt.core.dom.PackageDeclaration node) {
				Name name = node.getName();
				// this.names.add(name.getIdentifier());
				// System.out.println("Declaration of '"+name+"' at line"+cu.getLineNumber(name.getStartPosition()));
				return true; // do not continue to avoid usage info
			}

			public boolean visit(VariableDeclarationFragment node) {
				SimpleName name = node.getName();
				this.names.add(name.getIdentifier());

				WriteToTextFile("Declaration of '" + name + "' at line"
						+ cu.getLineNumber(name.getStartPosition()));
				
//				System.out.println("Declaration of '" + name + "' at line"
//						+ cu.getLineNumber(name.getStartPosition()));
				return true; // do not continue to avoid usage info
			}

			public boolean visit(org.eclipse.jdt.core.dom.QualifiedName node) {

				SimpleName name = node.getName();

				Name n = node.getQualifier();

				WriteToTextFile("QualifiedName '" + n + "." + name
						+ "' at line"
						+ cu.getLineNumber(name.getStartPosition()));
				
//				System.out.println("QualifiedName '" + n + "." + name
//						+ "' at line"
//						+ cu.getLineNumber(name.getStartPosition()));

				return true;
			}

			public boolean visit(SimpleName node) {

//				if (node.getFullyQualifiedName().equals("R")) {
//					System.out.println("NodeName:"
//							+ node.getFullyQualifiedName() + ",NodeType:"
//							+ node.getNodeType());
//				}

				WriteToTextFile("NodeName:" + node.getFullyQualifiedName()
						+ ",NodeType:" + node.getNodeType());
				
//				System.out.println("NodeName:" + node.getFullyQualifiedName()
//						+ ",NodeType:" + node.getNodeType());

				if (this.names.contains(node.getIdentifier())) {
//					System.out.println("Usage of '" + node + "' at line "
//							+ cu.getLineNumber(node.getStartPosition()));
				}
				return true;
			}

		});

	}

}
